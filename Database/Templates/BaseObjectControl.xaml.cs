using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Database.Templates
{
    /// <summary>
    /// Interaction logic for BaseObject.xaml
    /// </summary>
    public partial class BaseObjectControl : Database.Superclasses.BaseObject
    {
        public int Id { get; protected set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public string Created { get; protected set; }
        public string Updated { get; protected set; }
        public byte[] Image { get; protected set; }

        public BaseObjectControl()
        {
            InitializeComponent();
        }

        public void UpdateTime()
        {
            Updated = string.Format("{0:d}", Updated);
        }
    }
}
