
using Footbally.Application.AI.DTOs.Input;

namespace Footbally.Application.AI.Prompts;

public static class CareerCoachPromptBuilder
{
    public static string SystemPrompt =>
        "Sen Footbally platformunun AI Kariyer Koçusun.\n" +
        "Görevin: Oyuncuya 7, 30 ve 90 günlük gelişim planı ve antrenman tavsiyesi üretmek.\n\n" +
        "KURALLAR:\n" +
        "- Profesyonel ama gerçekçi dil kullan.\n" +
        "- Veri yoksa belirt, uydurma yapma.\n" +
        "- Yanıt sadece JSON olsun.\n" +
        "- Türkçe yaz.\n\n" +
        "ÇIKTI FORMATI:\n" +
        "{\n" +
        "  \"sevenDayPlan\": \"\",\n" +
        "  \"thirtyDayPlan\": \"\",\n" +
        "  \"ninetyDayPlan\": \"\",\n" +
        "  \"trainingAdvice\": \"\",\n" +
        "  \"confidenceScore\": 0.0,\n" +
        "  \"usedDataFields\": [],\n" +
        "  \"missingDataFields\": [],\n" +
        "  \"adminReviewRequired\": false\n" +
        "}";

    public static string BuildUserPrompt(CareerCoachInput input)
    {
        var teams = input.TeamHistory?.Count > 0 ? string.Join(", ", input.TeamHistory) : "Belirtilmemiş";
        return "Kariyer gelişim planı üret:\n\n" +
               "Pozisyon: " + (input.Position ?? "Belirtilmemiş") + "\n" +
               "Seviye: " + (input.Level ?? "Belirtilmemiş") + "\n" +
               "Yaş: " + (input.Age?.ToString() ?? "Belirtilmemiş") + "\n" +
               "Hakkında: " + (input.About ?? "Belirtilmemiş") + "\n" +
               "Geçmiş Takımlar: " + teams + "\n" +
               "Video Var Mı: " + input.HasVideo + "\n" +
               "CV Var Mı: " + input.HasCv + "\n" +
               "Profil Tamamlanma: %" + input.ProfileCompletionPercent + "\n" +
               "Premium Üye: " + input.IsPremium + "\n\n" +
               "Yalnızca JSON formatında yanıt ver.";
    }
}