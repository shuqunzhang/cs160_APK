   M S B A M L   `   `   `  ����^  ZMicrosoft.Samples.Kinect.WpfViewers, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null�^clr-namespace:Microsoft.Samples.Kinect.WpfViewers;assembly=Microsoft.Samples.Kinect.WpfViewers#Microsoft.Samples.Kinect.WpfViewers  5   4    apk   apk.TabooWords   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      mmy^clr-namespace:Microsoft.Samples.Kinect.WpfViewers;assembly=Microsoft.Samples.Kinect.WpfViewers     5   4   -   5        $	��700��5      $
��1080��6     N� Title$  
TabooWords��6!    � 	Resources 5	       �� 
TargetType �� Property �� Value �� 
TargetType �� Padding �� CornerRadius �� Triggers	 P� Property
 P� Value �� 
TargetName��%O  '��       �� 5
   
   " ��6   .�5      ��  �@�OverridesDefaultStyle . 66   �� 5       � ��$ Segoe UI)�6+   �� 5       � ��$
 48px(�6)   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� $��border��6!   $��3q�6/   $ 0q�6C   
���   �$ 10��6c   ���#EEEEEE._�5      � .+�5   "   �� $��content��5   8   $��Center=�5   3   $��Center=�5      68   5      5       5      P� 5      	 �@�Tag$
 hover��65   . �5   "   �� $ border��6)    � �� �#AAAAAA6S   5      P� 5      	 �@�Tag$
 press��65   . �5   "   �� $ border��6)    � ��
 �����6S   �� 5   "    � ��
 �����6?   5      P� 5      	 � {�
 .  . �5    "   ��  �@J�Opacity$	 0.4Q�6<   5!      5#      5%      5&      5'      .��5)      �� -   5        $��cWords��5)      .+�5*   
   ;   5Microsoft.Samples.Kinect.WpfViewers.KinectColorViewer  -   5        $��kinectColorViewer1��5*   A    �� Left$	 668��6   
 �� Top$ 12��61   $	��300��6[   $	��400��6h   5   /Microsoft.Samples.Kinect.WpfViewers.ImageViewer   Kinect 6t   ��  �� ElementName$ kinectSensorChooser1�� �� Path$ Kinect�=   7Microsoft.Samples.Kinect.WpfViewers.KinectSensorChooser  5+   
   -   5        $��kinectSensorChooser1��5+   E   $	 318��6!   $
 1000��63   $	��328��6a   �� 5,   
   -   5        $��
backButton��5,   O   $ 12��6   $	 553��6"   $
��Back��63   $	��135��6B   $	��340��6a   $��48(�6m   6{   �� 5-   
   -   5        $��deleteButton��5-   R   $	 370��6   $	 553��6#   $��Delete��64   $	��135��6E   $	��340��6f   $��48(�6r   6�   �� 5.   
   -   5        $��	addButton��5.   S   $	 728��6   $	 553��6#   $��Add New��64   $	��135��6F   $	��340��6d   $��48(�6p   6~   �� 50   
   -   5        $��wordsListBox��50      $	��466��6&   $	��634��63   $ 12��6?   $ 80��6P   $��36(�6`   .@�51      �� 6   52      �� 54   
   -	   5        $��label1��54   S   $ 12��6   $ 12��6!   $��Taboo Words��61   $��84��6G   $	��500��6a   $��48(�6m   �� 55   
   -
   5        $��nameInputPanel��55   E   $	 270��6   $	 318��6'   $	��300��68   $	��600��6[   
�������$y�Hidden=�6z   .+�56      �� -   5        $��nameInputLabel��56      $��Say a taboo word.��6*   $��36(�6F   $��60��6T   �� 57      -   5        $��	nameInput��57   "   $��60��6   $	��400��63   $��25q�6?   $��36(�6K   58      �� 59   
   -   5        $��okButton��59   +   $��OK��6   $	��100��6   $	��200��6;   $��48(�6G   $	 590��6e   $	 487��6w   $y�Hidden=�6�   {�.  6�   �� 5:   
   -   5        $��cancelButton��5:   /   $��Cancel��6   $	��100��6"   $	��200��6C   $��48(�6O   $	 350��6q   $	 487��6�   $y�Hidden=�6�   {�.  6�   �� 5;   
   -   5        $��panelBorder��5;   !   $	��300��6   
���   �$	��600��6C   $	 270��6O   $	 318��6a   $��3��6r   $y�Hidden=�6�   5<      5=      