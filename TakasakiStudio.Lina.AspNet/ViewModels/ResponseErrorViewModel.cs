using System.Net;
using System.Text.Json.Serialization;

namespace TakasakiStudio.Lina.AspNet.ViewModels;

public record ResponseErrorViewModel(
    [property: JsonIgnore] HttpStatusCode HttpStatusCode, 
    [property: JsonPropertyOrder(2)] string Message)
{
    [JsonPropertyOrder(1)]
    public int Code => (int) HttpStatusCode;
}