using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace MangaWorkflow.SoapService.HuyNQ.SoapModels;

[DataContract]
public class LoginHuyNqRequest
{
    [DataMember]
    [Required(AllowEmptyStrings = false, ErrorMessage = "UserName is required.")]
    public string Email { get; set; } = default!;

    [DataMember]
    [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required.")]
    public string Password { get; set; } = default!;
}
