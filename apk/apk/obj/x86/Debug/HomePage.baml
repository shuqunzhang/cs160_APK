   M S B A M L   `   `   `  ����^  ZMicrosoft.Samples.Kinect.WpfViewers, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null�^clr-namespace:Microsoft.Samples.Kinect.WpfViewers;assembly=Microsoft.Samples.Kinect.WpfViewers#Microsoft.Samples.Kinect.WpfViewers  5   	    apk   apk.HomePage   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   mmy^clr-namespace:Microsoft.Samples.Kinect.WpfViewers;assembly=Microsoft.Samples.Kinect.WpfViewers     5   	   -   5        $	��xn1��5   s   $	��700��5   	   $
��1080��6     N� Title$  HomePage��6#    � 	Resources 5       �� 
TargetType �� Property �� Value �� 
TargetType �� Padding �� CornerRadius �� Triggers	 P� Property
 P� Value �� 
TargetName��%O  '��       �� 5   
   " ��6   .�5	      ��  �@�OverridesDefaultStyle . 66   �� 5
       � ��$ Segoe UI)�6+   �� 5       � ��$
 48px(�6)   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� $��border��6!   $��3q�6/   $ 0q�6C   
���   �$ 10��6c   ���#EEEEEE._�5      � .+�5   "   �� $��content��5   8   $��Center=�5   3   $��Center=�5      68   5      5       5      P� 5      	 �@�Tag$
 hover��65   . �5   "   �� $ border��6)    � �� �#AAAAAA6S   5      P� 5      	 �@�Tag$
 press��65   . �5   "   �� $ border��6)    � ��
 �����6S   �� 5   "    � ��
 �����6?   5      P� 5      	 � {�
 .  . �5   "   ��  �@J�Opacity$	 0.4Q�6<   5      5!      5#      5$      5%      .��5(      �� -   5        $��	cHomePage��5(      .+�5)   
   ;   5Microsoft.Samples.Kinect.WpfViewers.KinectColorViewer  -   5        $��kinectColorViewer1��5)   Y    �� Left$ 26��6   
 �� Top$ 47��60   $	��500��6@   $	��375��6L   5   /Microsoft.Samples.Kinect.WpfViewers.ImageViewer   Kinect 6s   ��  �� ElementName$ kinectSensorChooser1�� �� Path$ Kinect�=   7Microsoft.Samples.Kinect.WpfViewers.KinectSensorChooser  5*   
   -   5        $��kinectSensorChooser1��5*   C   $ 0��6!   $
 1000��61   $	��328��6_   �� 5+   
   -   5        $��beginButton��5+   ]   $	 556��6   $ 19��6#   $��Begin Presentation��63   $	��200��6P   $	��500��6p   $��48(�6|   6�   �� 5,   
   -   5        $��reviewButton��5,   _   $	 556��6   $	 249��6#   $��Review Presentation��64   $	��200��6R   $	��500��6s   $��48(�6   6�   �� 5-   
   -   5        $��settingsButton��5-   [   $	 556��6   $	 479��6#   $��Change Settings��64   $	��200��6N   $	��500��6q   $��48(�6}   6�   �� 5.   
   -   5        $��closeButton��5.   \   $ 26��6   $	 479��6"   $��Close Application��63   $	��200��6O   $	��500��6o   $��48(�6{   6�   5/      50      