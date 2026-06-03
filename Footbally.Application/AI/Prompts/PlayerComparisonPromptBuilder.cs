
using Footbally.Application.AI.DTOs.Input;

namespace Footbally.Application.AI.Prompts;

public static class PlayerComparisonPromptBuilder
{
    public static string SystemPrompt =>
        "Sen Footbally platformunun AI Oyuncu Karşılaştırma Sistemisin.\n" +
        "Görevin: Birden fazla oyuncuyu karşılaştırarak en uygun olanı önermek.\n\n" +
        "KURALLAR:\n" +
        "- Yalnızca verilen veriyi kullan.\n" +
        "- En uygun oyuncuyu önerirken nedenini açıkla.\n" +
        "- Yanıt sadece JSON olsun.\n" +
        "- Türkçe yaz.\n\n" +
        "ÇIKTI FORMATI:\n" +
        "{\n" +
        "  \"recommendedPlayerId\": \"\",\n" +
        "  \"recommendationReason\": \"\",\n" +
        "  \"details\": [\n" +
        "    { \"playerId\": \"\", \"summary\": \"\", \"strengths\": \"\", \"weaknesses\": \"\" }\n" +
        "  ],\n" +
        "  \"confidenceScore\": 0.0,\n" +
        "  \"usedDataFields\": [],\n" +
        "  \"missingDataFields\": [],\n" +
        "  \"adminReviewRequired\": false\n" +
        "}";

    public static string BuildUserPrompt(PlayerComparisonInput input)
    {
        var players = string.Join("\n", input.Players.Select(p =>
            "- PlayerId: " + p.PlayerId + ", Pozisyon: " + p.Position + ", Yaş: " + p.Age +
            ", Seviye: " + p.Level + ", Video: " + p.HasVideo + ", Rating: " + p.OverallRating +
            ", Güven: " + p.TrustScore + ", Profil: %" + p.ProfileCompletionPercent));

        return "Aşağıdaki oyuncuları karşılaştır:\n\n" + players + "\n\n" +
               "Yalnızca JSON formatında karşılaştırma yap.";
    }
}