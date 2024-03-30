// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file under the MIT license.
// Source code from https://github.com/dotnet/aspnetcore/blob/6dfaf9e2cff6cfa3aab0b7842fe02fe9f71e0f60/src/Mvc/Mvc.Razor/src/Infrastructure/DefaultFileVersionProvider.cs
//
// The MIT License (MIT)
// 
// Copyright (c) .NET Foundation and Contributors
// 
// All rights reserved.
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System.Security.Cryptography;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.FileProviders;

namespace TakasakiStudio.Lina.AspNet.Providers;

public class FileVersionProvider(IWebHostEnvironment hostingEnvironment) : IFileVersionProvider
{
    private const string VersionKey = "v";
    private IMemoryCache Cache { get; } = new MemoryCache(new MemoryCacheOptions()
    {
        SizeLimit = 10485760L
    });

    private IFileProvider FileProvider { get; } = hostingEnvironment.WebRootFileProvider;
    
    public string AddFileVersionToPath(PathString requestPathBase, string path)
    {
        var resolvedPath = path;

        var queryStringOrFragmentStartIndex = path.AsSpan().IndexOfAny('?', '#');
        if (queryStringOrFragmentStartIndex != -1)
            resolvedPath = path[..queryStringOrFragmentStartIndex];

        if (Uri.TryCreate(resolvedPath, UriKind.Absolute, out var uri) && !uri.IsFile)
            return path;

        if (Cache.TryGetValue<string>(path, out var value) && value is not null)
            return value;

        var cacheEntryOptions = new MemoryCacheEntryOptions();
        cacheEntryOptions.AddExpirationToken(FileProvider.Watch(resolvedPath));
        var fileInfo = FileProvider.GetFileInfo(resolvedPath);

        if (!fileInfo.Exists &&
            requestPathBase.HasValue &&
            resolvedPath.StartsWith(requestPathBase.Value, StringComparison.OrdinalIgnoreCase))
        {
            var requestPathBaseRelativePath = resolvedPath[requestPathBase.Value.Length..];
            cacheEntryOptions.AddExpirationToken(FileProvider.Watch(requestPathBaseRelativePath));
            fileInfo = FileProvider.GetFileInfo(requestPathBaseRelativePath);
        }

        value = fileInfo.Exists ? QueryHelpers.AddQueryString(path, VersionKey, GetHashForFile(fileInfo)) :
            path;

        cacheEntryOptions.SetSize(value.Length * sizeof(char));
        Cache.Set(path, value, cacheEntryOptions);
        return value;
    }

    private static string GetHashForFile(IFileInfo fileInfo)
    {
        using var readStream = fileInfo.CreateReadStream();
        var hash = SHA256.HashData(readStream);
        return WebEncoders.Base64UrlEncode(hash);
    }
}