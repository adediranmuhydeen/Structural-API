﻿namespace ApiWithAuth.Core.IService
{
    public interface IMailService
    {
        Task SendEmailAsync(string toEmail, string subject, string content);
    }
}
