import { Resend } from 'resend';

const resend = new Resend(process.env['RESEND_API_KEY'] as string);

export default async function handler(req: any, res: any) {
  if (req.method !== 'POST') {
    return res.status(405).json({ error: 'Method not allowed' });
  }

  try {
    const body =
      typeof req.body === 'string' ? JSON.parse(req.body) : req.body;

    const { to, subject, html } = body || {};

    if (!to || !subject || !html) {
      return res.status(400).json({ error: 'Eksik alanlar var' });
    }

    const data = await resend.emails.send({
      from: 'Footbally <no-reply@getfootbally.com>',
      to,
      subject,
      html,
    });

    return res.status(200).json({ success: true, data });
  } catch (err: any) {
    console.error('SEND MAIL ERROR:', err);
    return res.status(500).json({ error: err?.message || 'Send failed' });
  }
}
