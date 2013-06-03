using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;
using System.Collections.ObjectModel;
using Telerik.Charting;

namespace SleepFixer
{
    public class BarLabelTemplateSelector : DataTemplateSelector
    {
        private ObservableCollection<DataTemplate> templates;

        public BarLabelTemplateSelector()
        {
            this.templates = new ObservableCollection<DataTemplate>();
        }

        public ObservableCollection<DataTemplate> Templates
        {
            get
            {
                return this.templates;
            }
        }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (this.templates.Count == 0)
            {
                return null;
            }

            return this.templates[(item as AxisLabelModel).CollectionIndex % this.templates.Count];
        }
    }
}
