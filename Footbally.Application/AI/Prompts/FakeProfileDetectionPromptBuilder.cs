
using Footbally.Application.AI.DTOs.Input;

namespace Footbally.Application.AI.Prompts;

public static class FakeProfileDetectionPromptBuilder
{
    public static string SystemPrompt =>
        "Sen Footbally platformunun AI Sahte Profil Tespit Sistemisin.\n" +
        "Görevin: Hesap sinyallerini analiz ederek sahte profil risk ihtimalini belirlemek.\n\n" +
        "KURALLAR:\n" +
        "- Kesin suçlama yapma, yalnızca risk ihtimali döndür.\n" +
        "- adminReviewRequired her zaman true olsun.\n" +
        "- Yanıt sadece JSON olsun.\n" +
        "- Türkçe yaz.\n" +
        "- riskLevel: low, medium, high, critical\n\n" +
        "ÇIKTI FORMATI:\n" +
        "{\n" +
        "  \"riskLevel\": \"\",\n" +
        "  \"riskSignals\": [],\n" +
        "  \"summary\": \"\",\n" +
        "  \"confidenceScore\": 0.0,\n" +
        "  \"usedDataFields\": [],\n" +
        "  \"missingDataFields\": [],\n" +
        "  \"adminReviewRequired\": true\n" +
        "}";

    public static string BuildUserPrompt(FakeProfileDetectionInput input)
    {
        var accountAge = (DateTime.UtcNow - input.AccountCreatedAt).Days;
        return "Hesap sinyalleri:\n" +
               "Hesap Yaşı (Gün): " + accountAge + "\n" +
               "Fotoğraf Var Mı: " + input.HasPhoto + "\n" +
               "Video Var Mı: " + input.HasVideo + "\n" +
               "Profil Tamamlanma: %" + input.ProfileCompletionPercent + "\n" +
               "Duplicate Fotoğraf: " + input.DuplicatePhotoDetected + "\n" +
               "Duplicate Email: " + input.DuplicateEmailDetected + "\n" +
               "Duplicate Telefon: " + input.DuplicatePhoneDetected + "\n" +
               "Hızlı Hesap Açma: " + input.RapidAccountCreation + "\n" +
               "Önceki Uyarı Sayısı: " + input.PreviousWarningCount + "\n" +
               "Hakkında: " + (input.About ?? "Belirtilmemiş") + "\n\n" +
               "Yalnızca JSON formatında risk analizi yap.";
    }
}