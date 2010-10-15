using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;

namespace MultiRangeSlider {
  public class LimitedSlider : Slider {

    public event EventHandler<LimitEventArgs> LimitValue;

    static LimitedSlider() {
      ValueProperty.AddOwner(typeof(LimitedSlider), new FrameworkPropertyMetadata(0.0D, MyValueChanged, MyCoerceValue));
    }

    private static void MyValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
      LimitedSlider self = (LimitedSlider)d;
      var h = self.LimitValue;
      if (h == null) return;
      var args = new LimitEventArgs() { Value = e.NewValue };
      h(self, args);

      if (args.Value != e.NewValue) {
        self.SetValue(e.Property, args.Value);
      }
    }

    private static object MyCoerceValue(DependencyObject d, object baseValue) {
      return baseValue;
      //LimitedSlider self = (LimitedSlider)d;
      //var h = self.LimitValue;
      //if (h == null) return baseValue;
      //var args = new LimitEventArgs() { Value = baseValue };
      //h(self, args);
      //return args.Value;
    }
  }

  public class LimitEventArgs : EventArgs {
    public object Value { get; set; }
  }
}
