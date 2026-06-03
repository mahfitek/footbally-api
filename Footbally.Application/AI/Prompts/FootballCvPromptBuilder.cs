
using Footbally.Application.AI.DTOs.Input;

namespace Footbally.Application.AI.Prompts;

public static class FootballCvPromptBuilder
{
    public static string SystemPrompt =>
        "Sen Footbally platformunun AI Futbol CV Sistemisin.\n" +
        "Görevin: Oyuncu bilgilerini profesyonel futbol CV metnine dönüştürmek.\n\n" +
        "KURALLAR:\n" +
        "- Scoutların anlayacağı kısa, net, kurumsal metin üret.\n" +
        "- PDF export için uygun format kullan.\n" +
        "- Veri yoksa o alanı atla, uydurma yapma.\n" +
        "- Yanıt sadece JSON olsun.\n" +
        "- Türkçe yaz.\n\n" +
        "ÇIKTI FORMATI:\n" +
        "{\n" +
        "  \"cvText\": \"\",\n" +
        "  \"pdfReadyText\": \"\",\n" +
        "  \"confidenceScore\": 0.0,\n" +
        "  \"usedDataFields\": [],\n" +
        "  \"missingDataFields\": [],\n" +
        "  \"adminReviewRequired\": false\n" +
        "}";

    public static string BuildUserPrompt(FootballCvInput input)
    {
        var teams = input.TeamHistory?.Count > 0 ? string.Join(", ", input.TeamHistory) : "Belirtilmemiş";
        return "Futbol CV'si oluştur:\n\n" +
               "Ad: " + (input.FullName ?? "Belirtilmemiş") + "\n" +
               "Yaş: " + (input.Age?.ToString() ?? "Belirtilmemiş") + "\n" +
               "Boy: " + (input.Height?.ToString() ?? "Belirtilmemiş") + "\n" +
               "Kilo: " + (input.Weight?.ToString() ?? "Belirtilmemiş") + "\n" +
               "Pozisyon: " + (input.Position ?? "Belirtilmemiş") + "\n" +
               "Tercih Edilen Ayak: " + (input.PreferredFoot ?? "Belirtilmemiş") + "\n" +
               "Seviye: " + (input.Level ?? "Belirtilmemiş") + "\n" +
               "Şehir: " + (input.City ?? "Belirtilmemiş") + "\n" +
               "Hakkında: " + (input.About ?? "Belirtilmemiş") + "\n" +
               "Geçmiş Takımlar: " + teams + "\n" +
               "Video Var Mı: " + input.HasVideo + "\n" +
               "Serbest Oyuncu: " + input.FreeAgent + "\n\n" +
               "Yalnızca JSON formatında yanıt ver.";
    }
}