
using Footbally.Application.AI.DTOs.Input;

namespace Footbally.Application.AI.Prompts;

public static class SupportTicketPromptBuilder
{
    public static string SystemPrompt =>
        "Sen Footbally platformunun AI Destek Talebi Sistemisin.\n" +
        "Görevin: Destek taleplerini kategorilere ayırmak ve admin için cevap taslağı üretmek.\n\n" +
        "KATEGORİLER: odeme, profil, mac_iptali, premium, teknik_hata, sikayet, guvenlik, diger\n" +
        "ÖNCELİK: low, medium, high, critical\n\n" +
        "KURALLAR:\n" +
        "- Kullanıcıyı suçlayıcı dil kullanma.\n" +
        "- Admin cevap taslağı kısa ve çözüm odaklı olsun.\n" +
        "- Yanıt sadece JSON olsun.\n" +
        "- Türkçe yaz.\n\n" +
        "ÇIKTI FORMATI:\n" +
        "{\n" +
        "  \"category\": \"\",\n" +
        "  \"priority\": \"\",\n" +
        "  \"adminDraftReply\": \"\",\n" +
        "  \"confidenceScore\": 0.0,\n" +
        "  \"usedDataFields\": [],\n" +
        "  \"missingDataFields\": [],\n" +
        "  \"adminReviewRequired\": true\n" +
        "}";

    public static string BuildUserPrompt(SupportTicketInput input)
    {
        return "Destek talebi:\n\n" +
               "Konu: " + (input.Subject ?? "Belirtilmemiş") + "\n" +
               "Açıklama: " + (input.Description ?? "Belirtilmemiş") + "\n" +
               "Kullanıcı Rolü: " + (input.UserRole ?? "Belirtilmemiş") + "\n\n" +
               "Yalnızca JSON formatında sınıflandır ve cevap taslağı üret.";
    }
}