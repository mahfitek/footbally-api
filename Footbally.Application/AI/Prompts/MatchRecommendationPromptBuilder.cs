
using Footbally.Application.AI.DTOs.Input;

namespace Footbally.Application.AI.Prompts;

public static class MatchRecommendationPromptBuilder
{
    public static string SystemPrompt =>
        "Sen Footbally platformunun AI Maç Öneri Sistemisin.\n" +
        "Görevin: Oyuncuya uygun maçları önermek ve her öneri için neden önerildiğini açıklamak.\n\n" +
        "KURALLAR:\n" +
        "- Yalnızca sana verilen maç listesinden öner.\n" +
        "- Her öneri için neden önerildiğini açıkla.\n" +
        "- Uyumluluk skoru 0-100 arası ver.\n" +
        "- Yanıt sadece JSON olsun.\n" +
        "- Türkçe yaz.\n\n" +
        "ÇIKTI FORMATI:\n" +
        "{\n" +
        "  \"recommendations\": [\n" +
        "    { \"matchId\": \"\", \"reason\": \"\", \"compatibilityScore\": 0 }\n" +
        "  ],\n" +
        "  \"confidenceScore\": 0.0,\n" +
        "  \"usedDataFields\": [],\n" +
        "  \"missingDataFields\": [],\n" +
        "  \"adminReviewRequired\": false\n" +
        "}";

    public static string BuildUserPrompt(MatchRecommendationInput input)
    {
        var matches = string.Join("\n", input.AvailableMatches.Select(m =>
            "- MatchId: " + m.MatchId + ", Şehir: " + m.City + ", İlçe: " + m.District +
            ", Pozisyon: " + m.NeededPosition + ", Seviye: " + m.Level +
            ", Tarih: " + m.MatchDate.ToString("yyyy-MM-dd HH:mm")));

        return "Oyuncu bilgileri:\n" +
               "Şehir: " + (input.City ?? "Belirtilmemiş") + "\n" +
               "İlçe: " + (input.District ?? "Belirtilmemiş") + "\n" +
               "Pozisyon: " + (input.Position ?? "Belirtilmemiş") + "\n" +
               "Seviye: " + (input.Level ?? "Belirtilmemiş") + "\n" +
               "Güven Skoru: " + (input.TrustScore?.ToString("F1") ?? "Belirtilmemiş") + "\n\n" +
               "Mevcut Maçlar:\n" + (string.IsNullOrEmpty(matches) ? "Maç bulunamadı." : matches) + "\n\n" +
               "Yalnızca JSON formatında maç önerileri ver.";
    }
}