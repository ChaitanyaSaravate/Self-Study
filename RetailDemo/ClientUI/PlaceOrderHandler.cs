﻿//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Messages;
//using NServiceBus;
//using NServiceBus.Logging;

//namespace ClientUI
//{
//	class PlaceOrderHandler : IHandleMessages<PlaceOrder>
//	{
//		static ILog log = LogManager.GetLogger<PlaceOrderHandler>();

//		public Task Handle(PlaceOrder message, IMessageHandlerContext context)
//		{
//			log.Info($"Received PlaceOrder with OrderId = {message.OrderId}");
//			return Task.CompletedTask;
//		}
//	}
//}
