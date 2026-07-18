#nullable disable
using System.Runtime.Serialization;

namespace MangaWorkflow.SoapService.HuyNQ.SoapModels;

// Login response model. Password is intentionally omitted from the SOAP contract.
[DataContract]
public class UserAccountHuyNq
{
    [DataMember]
    public int UserAccountId { get; set; }

    [DataMember]
    public string UserName { get; set; }

    [DataMember]
    public string FullName { get; set; }

    [DataMember]
    public string Email { get; set; }

    [DataMember]
    public string Phone { get; set; }

    [DataMember]
    public string EmployeeCode { get; set; }

    [DataMember]
    public int RoleId { get; set; }

    [DataMember]
    public string RequestCode { get; set; }

    [DataMember]
    public DateTime? CreatedDate { get; set; }

    [DataMember]
    public string ApplicationCode { get; set; }

    [DataMember]
    public string CreatedBy { get; set; }

    [DataMember]
    public DateTime? ModifiedDate { get; set; }

    [DataMember]
    public string ModifiedBy { get; set; }

    [DataMember]
    public bool IsActive { get; set; }

    // JWT issued on successful login.
    [DataMember]
    public string AccessToken { get; set; }

    [DataMember]
    public DateTime? TokenExpiresAt { get; set; }
}
