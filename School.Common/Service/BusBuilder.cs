using Microsoft.AspNetCore.Hosting;
using RawRabbit;
using School.Common.Command.Interfaces;
using School.Common.Event.Interfaces;
using School.Common.Handler.Interfaces;
using School.Common.RabbitMq;
using School.Common.Service.Abstracts;

namespace School.Common.Service
{
    public class BusBuilder : BuilderBase
    {
        private readonly IWebHost _webHost;
        private IBusClient _bus;

        public BusBuilder(IWebHost webHost, IBusClient bus)
        {
            this._webHost = webHost;
            this._bus = bus;
        }

        public BusBuilder SubscribeToCommand<TCommand>() where TCommand : ICommand
        {
            var handler = (ICommandHandler<TCommand>)_webHost.Services
                .GetService(typeof(ICommandHandler<TCommand>));

            _bus.WithCommandHandlerAsync(handler);

            return this;
        }

        public BusBuilder SubscribeToEvent<TEvent>() where TEvent : IEvent
        {
            var handler = (IEventHandler<TEvent>)_webHost.Services
                .GetService(typeof(IEventHandler<TEvent>));

            _bus.WithEventHandlerAsync(handler);

            return this;
        }

        public override ServiceHost Build()
        {
            return new ServiceHost(_webHost);
        }
    }
}