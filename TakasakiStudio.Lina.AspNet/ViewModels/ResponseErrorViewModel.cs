using System.Net;
using System.Text.Json.Serialization;

namespace TakasakiStudio.Lina.AspNet.ViewModels;

public record ResponseErrorViewModel(
    [property: JsonIgnore, JsonPropertyOrder(1)] HttpStatusCode HttpStatusCode, 
    string Message)
{
    [JsonPropertyOrder(0)]
    public int Code => (int) HttpStatusCode;
}