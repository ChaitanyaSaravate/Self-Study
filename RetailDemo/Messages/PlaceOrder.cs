using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Messages
{
    public class PlaceOrder : NServiceBus.ICommand
    {
		public int OrderId { get; set; }
    }
}
