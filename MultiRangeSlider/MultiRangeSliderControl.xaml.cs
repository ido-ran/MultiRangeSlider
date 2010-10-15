using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace MultiRangeSlider {
  /// <summary>
  /// Interaction logic for MultiRangeSliderControl.xaml
  /// </summary>
  public partial class MultiRangeSliderControl : UserControl, INotifyPropertyChanged {
    public MultiRangeSliderControl() {
      InitializeComponent();
      Loaded += new RoutedEventHandler(MultiRangeSliderControl_Loaded);
    }

    void MultiRangeSliderControl_Loaded(object sender, RoutedEventArgs e) {
      OnPropertyChanged(string.Empty);
    }

    public Thickness Range1Margin {
      get {
        double range = Maximum - Minimum;
        var renderSize = Value1Slider.RenderSize;
        double ratio = renderSize.Width / range;

        double right = Value1 * ratio;
        return new Thickness(0, 0, renderSize.Width - right, 0);
      }
    }

    public Thickness Range2Margin {
      get {
        double range = Maximum - Minimum;
        var renderSize = Value1Slider.RenderSize;
        double ratio = renderSize.Width / range;

        double left = Value1 * ratio;
        double right = Value2 * ratio;
        return new Thickness(left, 0, renderSize.Width - right, 0);
      }
    }

    public Thickness Range3Margin {
      get {
        double range = Maximum - Minimum;
        var renderSize = Value1Slider.RenderSize;
        double ratio = renderSize.Width / range;

        double left = Value2 * ratio;
        double right = Value3 * ratio;
        return new Thickness(left, 0, renderSize.Width - right, 0);
      }
    }

    public Thickness Range4Margin {
      get {
        double range = Maximum - Minimum;
        var renderSize = Value1Slider.RenderSize;
        double ratio = renderSize.Width / range;

        double left = Value3 * ratio;
        return new Thickness(left, 0, 0, 0);
      }
    }

    public double Minimum {
      get { return (double)GetValue(MinimumProperty); }
      set { SetValue(MinimumProperty, value); }
    }

    public static readonly DependencyProperty MinimumProperty =
        DependencyProperty.Register("Minimum", typeof(double), typeof(MultiRangeSliderControl), new UIPropertyMetadata(0d));

    public double Value1 {
      get { return (double)GetValue(Value1Property); }
      set { SetValue(Value1Property, value); }
    }

    public static readonly DependencyProperty Value1Property =
        DependencyProperty.Register("Value1", typeof(double), typeof(MultiRangeSliderControl), new FrameworkPropertyMetadata(4d, Value1Changed, CoerceValue1));

    public double Value2 {
      get { return (double)GetValue(Value2Property); }
      set { SetValue(Value2Property, value); }
    }

    public static readonly DependencyProperty Value2Property =
        DependencyProperty.Register("Value2", typeof(double), typeof(MultiRangeSliderControl), new FrameworkPropertyMetadata(6d, Value2Changed, CoerceValue2));

    public double Value3 {
      get { return (double)GetValue(Value3Property); }
      set { SetValue(Value3Property, value); }
    }

    public static readonly DependencyProperty Value3Property =
        DependencyProperty.Register("Value3", typeof(double), typeof(MultiRangeSliderControl), new FrameworkPropertyMetadata(8d, Value3Changed, CoerceValue3));


    public double Maximum {
      get { return (double)GetValue(MaximumProperty); }
      set { SetValue(MaximumProperty, value); }
    }

    public static readonly DependencyProperty MaximumProperty =
        DependencyProperty.Register("Maximum", typeof(double), typeof(MultiRangeSliderControl), new UIPropertyMetadata(10d));


    private void LimitValue1(object sender, LimitEventArgs e) {
      var v = (double)e.Value;
      if (v > Value2) e.Value = Value2;

      OnPropertyChanged("Range1Margin");
      OnPropertyChanged("Range2Margin");
    }

    private void LimitValue2(object sender, LimitEventArgs e) {
      var v = (double)e.Value;
      if (v > Value3) e.Value = Value3;
      else if (v < Value1) e.Value = Value1;

      OnPropertyChanged("Range2Margin");
      OnPropertyChanged("Range3Margin");
    }

    private void LimitValue3(object sender, LimitEventArgs e) {
      var v = (double)e.Value;
      if (v < Value2) e.Value = Value2;

      OnPropertyChanged("Range3Margin");
      OnPropertyChanged("Range4Margin");
    }


    private static void Value1Changed(DependencyObject d, DependencyPropertyChangedEventArgs e) {
      var self = (MultiRangeSliderControl)d;
      self.CoerceValue(Value2Property);
    }

    private static void Value2Changed(DependencyObject d, DependencyPropertyChangedEventArgs e) {
      var self = (MultiRangeSliderControl)d;
      self.CoerceValue(Value1Property);
      self.CoerceValue(Value3Property);
    }

    private static void Value3Changed(DependencyObject d, DependencyPropertyChangedEventArgs e) {
      var self = (MultiRangeSliderControl)d;
      self.CoerceValue(Value2Property);
    }

    private static object CoerceValue1(DependencyObject d, object baseValue) {
      var self = (MultiRangeSliderControl)d;
      var v = (double)baseValue;
      if (v > self.Value2) return self.Value2;
      else return baseValue;
    }

    private static object CoerceValue2(DependencyObject d, object baseValue) {
      var self = (MultiRangeSliderControl)d;
      var v = (double)baseValue;
      if (v > self.Value3) return self.Value3;
      else if (v < self.Value1) return self.Value1;
      else return baseValue;
    }

    private static object CoerceValue3(DependencyObject d, object baseValue) {
      var self = (MultiRangeSliderControl)d;
      var v = (double)baseValue;
      if (v < self.Value2) return self.Value2;
      else return baseValue;
    }

    #region INotifyPropertyChanged Members

    public event PropertyChangedEventHandler PropertyChanged;

    private void OnPropertyChanged(string name) {
      var h = PropertyChanged;
      if (h != null) h(this, new PropertyChangedEventArgs(name));
    }
    #endregion
  }
}
