   M S B A M L   `   `   `  ���� O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  M ISharedResx_NetCore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullS<clr-namespace:SharedResx_NetCore;assembly=SharedResx_NetCoreSharedResx_NetCore  DynamicDatagridColumns#   DynamicDatagridColumns.MsgBox   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     6sys&clr-namespace:System;assembly=mscorlib     Ldnc<clr-namespace:SharedResx_NetCore;assembly=SharedResx_NetCore     -     �Name$  _MsgBox�� � Topmost$
 True�� � WindowStyle$
 None=� � AllowsTransparency$
 True��
������  � Title$ MsgBox��$	��300��$	��350��'  !System.Windows.Shell.WindowChrome   WindowChrome      GlassFrameThickness$ 10q�   ResizeBorderThickness$ 10q�   UseAeroCaptionButtons$
 True��	   CaptionHeight$	 16Q�
 � 	Resources
  �� MergedDictionaries �� Source   _IndexNo��  �� $< 6/SharedResx_NetCore;component/Styles/ButtonStyles.xaml?�%   &	        ��0��.���� 
�������$��2q� �� CornerRadius$ 10��$��Stretch=�
����·�$��Stretch=�._��� 
�������$��10q�$ 10��$��Stretch=�
����·�$��Stretch=����� .���  �� Property � �� �� Value
 ��·���  � ��
 ��·���  � ��$ 5q�._�� 
�����_�$��Stretch=�$	��0,2q� � RowDefinitions �� $��30 ��� $��20 ��� $��* ��� $��40 � � ColumnDefinitions �� $��1* ��� $��30 ��� $��1* �.+�� $��0��
����·�$��3�� J� IsHitTestVisible$
 True��.+���  � Cursor$
 Hand��$��COLUMNS SELECTION��$
��Bold"�$��14(�$��Center=�$��Center=��� -   $��20��$��20��$��X��$��0q�
�������$��2q�  InformButton2#��[ $��Right=��� $��1��
�����_�._��� $��INCLUDED��$
��Bold"�$��Center=�$��Center=��� $��1��$��2��
�����_�._��� $��Center=�$��Center=�$��EXCLUDED��$
��Bold"��� $��2��$��5q�$��Stretch=�
�������$��Stretch=�._��� -    ��Name$ _incuededColumns��$
��Bold"��� $��2��$��1�� �� Orientation$ Vertical=�$��0q�$��Center=�.+��� -   $��
btnInclude��$	��0,2q�
�������  TranformBackwd#z�[ #��[ .��F� 
���   �"������- ��- рr�� � ���- @o@	 �� Stretch$
 Fill=�$
��16.5���� -    ��Name$ 
btnExclude��$	��0,2q�
�������  TranformForwd#z�[ #��[ .��F� 
���   �"������- ��- � �  � ���- @o@	$
 Fill=�$
��16.5���� $��2��$��2��$��5q�$��Stretch=�
�������$��Stretch=�._��� -   $ _excuededColumns��$
��Bold"����� *IsUniform.SelectedColumns+ �� UpdateSourceTrigger$ PropertyChanged=��� $��3��$��3��
����·�._��� -   $ _btnOk��$	��100��
�������$��30��$��Ok��#���   #��[ $	��0,5q�$��Center=����� *
CmdUniform+