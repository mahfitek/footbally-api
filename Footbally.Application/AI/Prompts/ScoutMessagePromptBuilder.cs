
using Footbally.Application.AI.DTOs.Input;

namespace Footbally.Application.AI.Prompts;

public static class ScoutMessagePromptBuilder
{
    public static string SystemPrompt =>
        "Sen Footbally platformunun AI Scout Mesaj Sistemisin.\n" +
        "Görevin: Scout veya kulüp adına oyuncuya profesyonel ilk iletişim mesajı yazmak.\n\n" +
        "KURALLAR:\n" +
        "- Kısa, güvenilir, ciddi ve doğal Türkçe kullan.\n" +
        "- Gereksiz vaat verme.\n" +
        "- Yanıt sadece JSON olsun.\n\n" +
        "ÇIKTI FORMATI:\n" +
        "{\n" +
        "  \"messageText\": \"\",\n" +
        "  \"confidenceScore\": 0.0,\n" +
        "  \"usedDataFields\": [],\n" +
        "  \"missingDataFields\": [],\n" +
        "  \"adminReviewRequired\": false\n" +
        "}";

    public static string BuildUserPrompt(ScoutMessageInput input)
    {
        return "Scout bilgileri:\n" +
               "Scout Adı: " + (input.ScoutName ?? "Belirtilmemiş") + "\n" +
               "Kulüp/Organizasyon: " + (input.ClubOrOrganization ?? "Belirtilmemiş") + "\n\n" +
               "Oyuncu bilgileri:\n" +
               "Oyuncu Adı: " + (input.PlayerName ?? "Belirtilmemiş") + "\n" +
               "Pozisyon: " + (input.PlayerPosition ?? "Belirtilmemiş") + "\n\n" +
               "Amaç: " + (input.Purpose ?? "Belirtilmemiş") + "\n\n" +
               "Yalnızca JSON formatında mesaj üret.";
    }
}