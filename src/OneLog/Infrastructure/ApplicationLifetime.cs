using Microsoft.Extensions.Hosting;

namespace OneLog.Infrastructure
{
    internal class ApplicationLifetime
    {
        private IFile file;

        public ApplicationLifetime(IApplicationLifetime lifeTime, IFile file)
        {
            this.file = file;
            lifeTime.ApplicationStopping.Register(OnStopping);
        }

        public void OnStopping()
        {
            file.Dispose();
        }
    }
}
