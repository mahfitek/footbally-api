
using Footbally.Application.AI.DTOs.Input;

namespace Footbally.Application.AI.Prompts;

public static class VideoSummaryPromptBuilder
{
    public static string SystemPrompt =>
        "Sen Footbally platformunun AI Video Özet Sistemisin.\n" +
        "Görevin: Video açıklaması ve etiketlerine göre oyuncu performans özeti çıkarmak.\n\n" +
        "KURALLAR:\n" +
        "- Gerçek video görüntüsü yoksa bunu açıkça belirt.\n" +
        "- Hareket, gol, asist, hız iddiası uydurma yapma.\n" +
        "- isRealVideoAnalysis alanını doğru set et.\n" +
        "- Yanıt sadece JSON olsun.\n" +
        "- Türkçe yaz.\n\n" +
        "ÇIKTI FORMATI:\n" +
        "{\n" +
        "  \"performanceSummary\": \"\",\n" +
        "  \"isRealVideoAnalysis\": false,\n" +
        "  \"dataSourceNote\": \"\",\n" +
        "  \"confidenceScore\": 0.0,\n" +
        "  \"usedDataFields\": [],\n" +
        "  \"missingDataFields\": [],\n" +
        "  \"adminReviewRequired\": false\n" +
        "}";

    public static string BuildUserPrompt(VideoSummaryInput input)
    {
        var tags = input.VideoTags?.Count > 0 ? string.Join(", ", input.VideoTags) : "Belirtilmemiş";
        return "Video bilgileri:\n" +
               "Açıklama: " + (input.VideoDescription ?? "Belirtilmemiş") + "\n" +
               "Etiketler: " + tags + "\n" +
               "Analiz Metni: " + (input.AnalysisText ?? "Belirtilmemiş") + "\n\n" +
               "Yalnızca JSON formatında performans özeti üret.";
    }
}