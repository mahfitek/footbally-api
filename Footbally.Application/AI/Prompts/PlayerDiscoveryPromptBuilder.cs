
using Footbally.Application.AI.DTOs.Input;

namespace Footbally.Application.AI.Prompts;

public static class PlayerDiscoveryPromptBuilder
{
    public static string SystemPrompt =>
        "Sen Footbally platformunun AI Oyuncu Keşif Sistemisin.\n" +
        "Görevin: Scout kriterlerine göre aday oyuncuları sıralamak ve uyum skoru üretmek.\n\n" +
        "KURALLAR:\n" +
        "- Yalnızca sana verilen oyuncu listesinden öner.\n" +
        "- Her oyuncu için uyum skoru, öneri nedeni, risk notu ve eksik bilgi yaz.\n" +
        "- Uyumluluk skoru 0-100 arası ver.\n" +
        "- Yanıt sadece JSON olsun.\n" +
        "- Türkçe yaz.\n\n" +
        "ÇIKTI FORMATI:\n" +
        "{\n" +
        "  \"rankedPlayers\": [\n" +
        "    { \"playerId\": \"\", \"compatibilityScore\": 0, \"recommendReason\": \"\", \"riskNote\": \"\", \"missingInfo\": \"\" }\n" +
        "  ],\n" +
        "  \"confidenceScore\": 0.0,\n" +
        "  \"usedDataFields\": [],\n" +
        "  \"missingDataFields\": [],\n" +
        "  \"adminReviewRequired\": false\n" +
        "}";

    public static string BuildUserPrompt(PlayerDiscoveryInput input)
    {
        var candidates = string.Join("\n", input.Candidates.Select(c =>
            "- PlayerId: " + c.PlayerId + ", Pozisyon: " + c.Position + ", Yaş: " + c.Age +
            ", Şehir: " + c.City + ", Seviye: " + c.Level + ", Ayak: " + c.PreferredFoot +
            ", Video: " + c.HasVideo + ", CV: " + c.HasCv +
            ", Güven: " + c.TrustScore + ", Profil: %" + c.ProfileCompletionPercent +
            ", Rating: " + c.OverallRating));

        return "Scout Kriterleri:\n" +
               "Pozisyon: " + (input.Position ?? "Belirtilmemiş") + "\n" +
               "Şehir: " + (input.City ?? "Belirtilmemiş") + "\n" +
               "Yaş Aralığı: " + (input.MinAge?.ToString() ?? "?") + "-" + (input.MaxAge?.ToString() ?? "?") + "\n" +
               "Seviye: " + (input.Level ?? "Belirtilmemiş") + "\n" +
               "Tercih Edilen Ayak: " + (input.PreferredFoot ?? "Belirtilmemiş") + "\n" +
               "Video Zorunlu: " + input.RequireVideo + "\n" +
               "CV Zorunlu: " + input.RequireCv + "\n\n" +
               "Aday Oyuncular:\n" + (string.IsNullOrEmpty(candidates) ? "Aday bulunamadı." : candidates) + "\n\n" +
               "Yalnızca JSON formatında sıralama yap.";
    }
}