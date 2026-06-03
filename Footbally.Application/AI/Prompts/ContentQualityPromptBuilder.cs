
using Footbally.Application.AI.DTOs.Input;

namespace Footbally.Application.AI.Prompts;

public static class ContentQualityPromptBuilder
{
    public static string SystemPrompt =>
        "Sen Footbally platformunun AI İçerik Kalite Sistemisin.\n" +
        "Görevin: Oyuncu profilinin scout'a uygun kalite seviyesini ölçmek.\n\n" +
        "KURALLAR:\n" +
        "- Kalite skoru 0-100 arası ver.\n" +
        "- qualityLabel: Zayıf (0-39), Orta (40-59), İyi (60-79), Mükemmel (80-100)\n" +
        "- Kullanıcıya iyileştirme önerileri ver.\n" +
        "- Yanıt sadece JSON olsun.\n" +
        "- Türkçe yaz.\n\n" +
        "ÇIKTI FORMATI:\n" +
        "{\n" +
        "  \"qualityScore\": 0,\n" +
        "  \"qualityLabel\": \"\",\n" +
        "  \"improvementSuggestions\": [],\n" +
        "  \"confidenceScore\": 0.0,\n" +
        "  \"usedDataFields\": [],\n" +
        "  \"missingDataFields\": [],\n" +
        "  \"adminReviewRequired\": false\n" +
        "}";

    public static string BuildUserPrompt(ContentQualityInput input)
    {
        var teams = input.TeamHistory?.Count > 0 ? string.Join(", ", input.TeamHistory) : "Belirtilmemiş";
        return "Profil kalitesi değerlendir:\n\n" +
               "Fotoğraf: " + input.HasPhoto + "\n" +
               "Video: " + input.HasVideo + "\n" +
               "CV: " + input.HasCv + "\n" +
               "Pozisyon: " + (input.Position ?? "Belirtilmemiş") + "\n" +
               "Boy: " + (input.Height?.ToString() ?? "Belirtilmemiş") + "\n" +
               "Kilo: " + (input.Weight?.ToString() ?? "Belirtilmemiş") + "\n" +
               "Geçmiş Takımlar: " + teams + "\n" +
               "Hakkında: " + (input.About ?? "Belirtilmemiş") + "\n" +
               "Profil Tamamlanma: %" + input.ProfileCompletionPercent + "\n\n" +
               "Yalnızca JSON formatında kalite değerlendirmesi yap.";
    }
}