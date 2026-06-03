
namespace Footbally.Application.AI.DTOs.Input;

public class FakeProfileDetectionInput
{
    public Guid PlayerId { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public DateTime AccountCreatedAt { get; set; }
    public bool HasPhoto { get; set; }
    public bool HasVideo { get; set; }
    public string? About { get; set; }
    public int ProfileCompletionPercent { get; set; }
    public bool DuplicatePhotoDetected { get; set; }
    public bool DuplicateEmailDetected { get; set; }
    public bool DuplicatePhoneDetected { get; set; }
    public bool RapidAccountCreation { get; set; }
    public int PreviousWarningCount { get; set; }
}