
using Footbally.Application.AI.DTOs.Input;

namespace Footbally.Application.AI.Prompts;

public static class TrustScorePromptBuilder
{
    public static string SystemPrompt => """
        Sen Footbally platformunun AI Güven Skoru Sistemisin.
        Görevin: Oyuncu davranış sinyallerine göre 0-100 arası güven skoru üretmek.

        KURALLAR:
        - Skoru kullanıcıya cezalandırıcı şekilde gösterme, admin ve eşleşme algoritması için üret.
        - Eksik veri varsa confidenceScore düşük döndür.
        - Yanıt sadece JSON olsun, başka hiçbir şey yazma.
        - Türkçe yaz.
        - scoreLabel: Düşük (0-39), Orta (40-59), İyi (60-79), Yüksek (80-100)

        ÇIKTI FORMATI:
        {
          "score": 0.0,
          "scoreLabel": "",
          "positiveSignals": [],
          "negativeSignals": [],
          "confidenceScore": 0.0,
          "usedDataFields": [],
          "missingDataFields": [],
          "adminReviewRequired": false
        }
        """;

    public static string BuildUserPrompt(TrustScoreInput input)
    {
        var accountAge = (DateTime.UtcNow - input.AccountCreatedAt).Days;

        return $"""
            Aşağıdaki oyuncu davranış verisi üzerinden güven skoru üret:

            Profil Tamamlanma: %{input.ProfileCompletionPercent}
            Hesap Doğrulandı Mı: {input.IsVerified}
            Şikayet Sayısı: {input.ComplaintCount}
            Maça Gelmeme Sayısı: {input.NoShowCount}
            Olumlu Değerlendirme Sayısı: {input.PositiveReviewCount}
            Ödeme Sorunu Sayısı: {input.PaymentIssueCount}
            Admin Uyarısı Sayısı: {input.AdminWarningCount}
            Hesap Yaşı (Gün): {accountAge}
            Toplam Maç Sayısı: {input.MatchCount}

            Yalnızca JSON formatında güven skoru üret.
            """;
    }
}