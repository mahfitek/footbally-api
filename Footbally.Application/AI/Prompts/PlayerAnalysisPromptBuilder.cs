using Footbally.Application.AI.DTOs.Input;

namespace Footbally.Application.AI.Prompts;

public static class PlayerAnalysisPromptBuilder
{
    public static string SystemPrompt =>
        "Sen Footbally platformunun AI Oyuncu Analiz Sistemisin.\n" +
        "Görevin: Oyuncu verisine göre güçlü yönler, zayıf yönler, gelişim alanları, oyun tarzı ve scout özeti üretmek.\n\n" +
        "KURALLAR:\n" +
        "- Yalnızca verilen veriyi kullan. Eksik veri varsa belirt, uydurma yapma.\n" +
        "- Yanıt sadece JSON olsun.\n" +
        "- Türkçe, profesyonel dil kullan.\n\n" +
        "ÇIKTI FORMATI:\n" +
        "{\n" +
        "  \"strengths\": \"\",\n" +
        "  \"weaknesses\": \"\",\n" +
        "  \"developmentAreas\": \"\",\n" +
        "  \"playingStyle\": \"\",\n" +
        "  \"scoutSummary\": \"\",\n" +
        "  \"confidenceScore\": 0.0,\n" +
        "  \"usedDataFields\": [],\n" +
        "  \"missingDataFields\": [],\n" +
        "  \"adminReviewRequired\": false\n" +
        "}";

    public static string BuildUserPrompt(PlayerAnalysisInput input)
    {
        var teams = input.TeamHistory?.Count > 0 ? string.Join(", ", input.TeamHistory) : "Belirtilmemiş";
        return "Oyuncu analizi yap:\n\n" +
               "Pozisyon: " + (input.Position ?? "Belirtilmemiş") + "\n" +
               "Seviye: " + (input.Level ?? "Belirtilmemiş") + "\n" +
               "Yaş: " + (input.Age?.ToString() ?? "Belirtilmemiş") + "\n" +
               "Boy: " + (input.Height?.ToString() ?? "Belirtilmemiş") + "\n" +
               "Kilo: " + (input.Weight?.ToString() ?? "Belirtilmemiş") + "\n" +
               "Tercih Edilen Ayak: " + (input.PreferredFoot ?? "Belirtilmemiş") + "\n" +
               "Şehir: " + (input.City ?? "Belirtilmemiş") + "\n" +
               "Hakkında: " + (input.About ?? "Belirtilmemiş") + "\n" +
               "Geçmiş Takımlar: " + teams + "\n" +
               "Video Var Mı: " + input.HasVideo + "\n" +
               "CV Var Mı: " + input.HasCv + "\n" +
               "Profil Tamamlanma: %" + input.ProfileCompletionPercent + "\n\n" +
               "Yalnızca JSON formatında yanıt ver.";
    }
}