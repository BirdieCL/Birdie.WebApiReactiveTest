using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Birdie.WebApiReactiveTest.Utils
{
    public class ErrorHandler
    {
        private Subject<Exception> _errorSubject;

        public ErrorHandler()
        {
            Inicializar();
        }

        public void Inicializar()
        {
            _errorSubject = new Subject<Exception>();

            _errorSubject
                .Buffer(TimeSpan.FromSeconds(10))
                .Where(errors => errors.Count > 0)
                .Subscribe(errors =>
                {
                    Console.WriteLine($"{DateTime.Now}: Se han acumulado {errors.Count} errores en los últimos 10 segundos.");
                    foreach (var err in errors)
                    {
                        Console.WriteLine($"error: {err.Message}");
                    }
                    // Aquí enviarías tu mensaje
                });
        }

        public void OnError(Exception ex)
        {
            _errorSubject.OnNext(ex);
        }
    }
}
