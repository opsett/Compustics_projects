   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5   	   M ISharedResx_NetCore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullS<clr-namespace:SharedResx_NetCore;assembly=SharedResx_NetCoreSharedResx_NetCore 5   	    DynamicDatagridColumns#   DynamicDatagridColumns.MsgBox   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6sys&clr-namespace:System;assembly=mscorlib     5   	   Ldnc<clr-namespace:SharedResx_NetCore;assembly=SharedResx_NetCore     5   	   -   5          �Name$  _MsgBox��5	   2    � Topmost$
 True��5       � WindowStyle$
 None=�6)    � AllowsTransparency$
 True��6<   
������  � Title$ MsgBox��5	   	   $	��300��6   $	��350��6%   '  !System.Windows.Shell.WindowChrome   WindowChrome 5
        5   
      GlassFrameThickness$ 10q�6      ResizeBorderThickness$ 10q�5         UseAeroCaptionButtons$
 True��5      	   CaptionHeight$	 16Q�5      
 � 	Resources
 5       �� MergedDictionaries �� Source   _IndexNo�� 5   
    5      �� 5      $< 6/SharedResx_NetCore;component/Styles/ButtonStyles.xaml?�6%   %#   &	        ��5      0��6-   5      .��5      �� 
�������$��2q�6%    �� CornerRadius$ 10��6;   $��Stretch=�6N   
����·�$��Stretch=�6�   ._�5   
   �� 
�������$��10q�6)   $ 10��6@   $��Stretch=�6S   
����·�$��Stretch=�6�   ��5       �� 5"      .�5$      ��  �� Property � �� �� Value
 ��·�6=   �� 5%       � ��
 ��·�6<   �� 5&       � ��$ 5q�6?   5(      ._�5/      � 
�����_�$��Stretch=�6*   $	��0,2q�6H    � RowDefinitions 50      �� 51      $��30 �6$   �� 52      $��20 �6$   �� 53      $��* �6$   �� 54      $��40 �6$    � ColumnDefinitions 56      �� 57      $��1* �6'   �� 58      $��30 �6'   �� 59      $��1* �6'   .+�5;      � $��0��6   
����·�$��3��6:    J� IsHitTestVisible$
 True��6N   .+�5<      ��  � Cursor$
 Hand��6$   $��COLUMNS SELECTION��62   $
��Bold"�6K   $��14(�6^   $��Center=�6l   $��Center=�6�   �� 5=      -   5        $��20��5=      $��20��6%   $��X��6F   $��0q�6R   
�������$��2q�6t     InformButton2#��[ 5>      $��Right=�6>   5@      �� 5A      $��1��6   
�����_�._�5B      �� $��INCLUDED��6    $
��Bold"�60   $��Center=�6B   $��Center=�6]   5C      �� 5D      $��1��6   $��2��6&   
�����_�._�5E      �� $��Center=�6    $��Center=�6<   $��EXCLUDED��6Y   $
��Bold"�6i   5F      �� 5G      $��2��6   $��5q�6&   $��Stretch=�6:   
�������$��Stretch=�6l   ._�5H      �� -   5         ��Name$ _incuededColumns��5H      $
��Bold"�69   5I      �� 5J      $��2��6   $��1��6*    �� Orientation$ Vertical=�6:   $��0q�6Q   $��Center=�6]   .+�5K      �� -   5        $��
btnInclude��5K   *   $	��0,2q�6   
�������  TranformBackwd#z�[ 6n   #��[ 6�   .��5L      F� 
���   �"������- ��- рr�� � ���- @o@	 �� Stretch$
 Fill=�6J   $
��16.5��6Y   5M      �� 5O      -   5         ��Name$ 
btnExclude��5O   *   $	��0,2q�6   
�������  TranformForwd#z�[ 5P      #��[ 6J   .��5Q      F� 
���   �"������- ��- � �  � ���- @o@	$
 Fill=�6J   $
��16.5��6Y   5R      5S      �� 5T      $��2��6   $��2��6&   $��5q�66   $��Stretch=�6K   
�������$��Stretch=�6~   ._�5U      �� -   5        $ _excuededColumns��5U      $
��Bold"�69   ��6L   �� *IsUniform.SelectedColumns+ �� UpdateSourceTrigger$ PropertyChanged=�5V      �� 5X      $��3��6   $��3��6&   
����·�._�5Y      �� -   5        $ _btnOk��5Y   )   $	��100��6   
�������$��30��6e   $��Ok��6t   #���   5Z   9   #��[ 6i   $	��0,5q�6�   $��Center=�6�   ��6   �� *
CmdUniform+6�   5[      5]      5b      5c      5d      