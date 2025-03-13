using BE_Biblioteca.Models;
using FluentEmail.Core;

namespace BE_Biblioteca.Services
{
    public class EmailService
    {
        private readonly IFluentEmail _fluentEmail;
        public EmailService(IFluentEmail fluentEmail)
        {
            _fluentEmail = fluentEmail;
        }
        public async Task<bool> SendLoanEmailAsync(Prestito prestito)
        {
            var result = await _fluentEmail
                .To(prestito.UserEmail)
                .Subject($"Prestito Libro: {prestito.Book.Title}")
                .Body($"Gentile {prestito.Username},\n\nLe confermiamo il prestito del libro '{prestito.Book.Title}' di {prestito.Book.Author}.\nIl libro dovrà essere riconsegnato entro: {prestito.LimiteRestituzione}.\n\nGrazie per aver utilizzato la nostra piattaforma.")
                .SendAsync();

            return result.Successful;
        }
        public async Task<bool> SendReturnEmailAsync(Prestito prestito)
        {
            var result = await _fluentEmail
                .To(prestito.UserEmail)
                .Subject($"Restituzione Libro: {prestito.Book.Title}")
                .Body($"Gentile {prestito.Username},\n\nLe confermiamo la restituzione del libro '{prestito.Book.Title}' di {prestito.Book.Author} in data {prestito.DataRestituzione}.\n\nGrazie per aver utilizzato la nostra piattaforma.\n\nEpiLibrary")
                .SendAsync();

            return result.Successful;
        }

        public async Task<bool> SendReminderEmailAsync(Prestito prestito)
        {
            var result = await _fluentEmail
                .To(prestito.UserEmail)
                .Subject($"PROMEMORIA RESTITUZIONE: {prestito.Book.Title}")
                .Body($"Gentile {prestito.Username},\n\nLa informiamo che il libro '{prestito.Book.Title}' di {prestito.Book.Author} dovrà essere restituito entro {prestito.LimiteRestituzione} \n La preghiamo di procedere alla restituzione. \n\nGrazie per la collaborazione.\n\nEpiLibrary")
                .SendAsync();

            return result.Successful;
        }
    }
}
