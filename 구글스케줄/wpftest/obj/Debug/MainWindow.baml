   M S B A M L   `   `   `  ����B  >wpftest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:wpftestwpftest  5   	       wpftest.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   'localclr-namespace:wpftest     5   	     � Title$  
MainWindow��5   	   $
��1000��6   $
��1000��6*    � 	Resources 5         Storyboard1  Storyboard2 �� TargetProperty �� 
TargetName9  3System.Windows.Media.Animation.EasingDoubleKeyFrame O� KeyTime O� Value��%s  &	        &	      �� 5      6   �� 5      .��5	      U� $U O(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.X)�6#   $ button��6�   .\�5
        $ 0��6   $
 -131Q�6'     5      $ 0:0:0.4��6   $
 -130Q�6-   5      U� 5      $U O(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.Y)�6#   $ button��6�   .\�5        $ 0��6   $ 0Q�6'     5      $ 0:0:10��6   $	 100Q�6,   5      U� 5      $U O(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.X)�6#   $ 	textBlock��6�   .\�5        $ 0:0:0.4��6   $ -2Q�6-   5      U� 5      $U O(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.Y)�6#   $ 	textBlock��6�   .\�5        $ 0:0:0.4��6   $ 1Q�6-   5      5      5       � Triggers 5      :� 5       :� RoutedEvent$ FrameworkElement.Loaded��6   .[�5      �� #��[  6   �� 5      #��[ 6   5      .��5!      �  � RowDefinitions 5"   
   �� 5#      $
��100* �6   �� 5$      $	��30* �6   	 � ColumnDefinitions	 5&   
   �� 5'      $
��260* �6   �� 5(      $
��150* �6   .+�5+   
   �� -   5        
 ��Name$
 	textBlock��5+       J� RenderTransformOrigin$ 0.5,0.5&�6'   z�5,      V� 5-      .~�5.      �� �� 5/      �� 50      T� 51      52      �� 53   '   �� 6-   �� 69   �� 6?   �� 6K   $�asdfasdf��6O   �� 6a   �� 6m   $� ��6q   6|   �� 55      -   5         ��Name$ button��55      
������� K� ZIndex$	 103��6.   $��30��6A   $��30��6L   $��asdfasdf��6X   $ 0.5,0.5&�6k   z�56      V� 57      .~�58      �� �� 59      �� 5:      T� 5;      5<      5=         wpftest.UserControl1  5?   
   -   5          Name$ Uc��5?         wpftest.UserControl1   Loaded$ UserControl1_Loaded��6e   $��0��6*   $��0��6:   $ 50,50&�6G   $��0q�6�   5@      �� 5A   
   $��0��6   $��1��6    ��5B      �� 5C       �� Source$ NewFolder1/아이유.png��6     �� 
SourceRect$ 100 100 200 200��6<   5E      5F      5G      