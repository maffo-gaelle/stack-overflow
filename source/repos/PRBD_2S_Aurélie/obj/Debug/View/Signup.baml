   M S B A M L   `   `   `  ����I  EPRBD_Framework, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG4clr-namespace:PRBD_Framework;assembly=PRBD_FrameworkPRBD_Framework  5   	   K GPRBD_2S_Aurélie, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:PRBD_2S_Aurélie.PropertiesPRBD_2S_Aurélie.Properties 5   	      PRBD_2S_Aurélie.Signup   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Bf4clr-namespace:PRBD_Framework;assembly=PRBD_Framework     5   	   7p)clr-namespace:PRBD_2S_Aurélie.Properties     5   	   -   5           PRBD_Framework.WindowBase   Name$  window��5   	    � Title$ Inscription��5
   	    � WindowStartupLocation$ CenterScreen=�6    � 
ResizeMode$ NoResize=�5   I   $	��700��5   	   $
��auto��6    � SizeToContent$ WidthAndHeight=�5   	    +� FocusedElement 5   	   ��  �� ElementName$ txtUserName��.��5      � .+�5   
   � $��10q�6    � ColumnDefinitions 5      �� 5      $
��Auto �6#   �� 5       � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      .+�5      �� +  %PRBD_2S_Aurélie.Properties.Resources	   Signup_UserName#��Z	 6   $
��Bold"�6E   $��1��6W   $��Center=�6d   $��5q�6   #   PRBD_Framework.ValidatedField  5       $��1��6   $��1��6,   $��Center=�6<   .+�5!      �� -   5        
 ��Name$
 txtUserName��5!       �� TextWrapping$
 Wrap=�6/   $��1��6C   ��5"      �� *
UserName+ �� UpdateSourceTrigger$ PropertyChanged=�5#      �� 5%         Signup_Password#��Z 6   $
��Bold"�6E   $��2��6W   $��Center=�6d   $��5q�6     5&      $��2��6   $��1��6,   $��Center=�6<   .+�5'      G� -   5         G�Name$ txtPassword��5'      #   PRBD_Framework.PasswordHelper   Attach . $��2��5*      $��1��6#   $��Center=�63      Password 5)      �� *
Password+$ PropertyChanged=�5*   3   5+      �� 5-         SignupPassword_Confirm#��Z 6   $
��Bold"�6L   $��3��6^   $��Center=�6k   $��5q�6�     5.      $��3��6   $��1��6,   $��Center=�6<   .+�5/      G� -   5        $ txtPasswordConfirm��5/       . $��3��52      $��1��6#   $��Center=�63    51      �� *PasswordConfirm+$ PropertyChanged=�52   3   53      �� 55         Signup_FullName#��Z 6   $
��Bold"�6E   $��4��6W   $��Center=�6d   $��5q�6     56      $��4��6   $��1��6,   $��Center=�6<   .+�57      �� -   5        $
 txtFullName��57      $
 Wrap=�6/   $��1��6C   ��58      �� *
FullName+$ PropertyChanged=�59      �� 5;         Signup_Email#��Z 6   $
��Bold"�6B   $��5��6T   $��Center=�6a   $��5q�6|     5<      $��5��6   $��1��6,   $��Center=�6<   .+�5=      �� -   5        $
 txtEmail��5=      $
 Wrap=�6,   $��1��6@   ��5>      �� *Email+$ PropertyChanged=�5?      ]� 5A      $��0,6,0,0q�6   $��6��6)   $��center=�66   $��Bottom=�6S   $��2��6o   $	��250��6�   .+�5B      �� -   5         ��Name$ 	btnCancel��5B         btn_annuler#��Z 6,   $��80��6u   $
��Bold"�6�   $	��5,1q�6�   ��6Y   �� *	Annuler+6�   �� 5C      -   5        $ btnSave��5C         btn_valider#��Z 6*   $��80��6w   $
��Bold"�6�   $��10,0,0,0q�6�   $	��5,1q�6�   ��6W   �� *Inscription+6�   5D      ]� 5E      $��0,14,0,0q�6   $��7��6*   $��2��67   .+�5F      �� $
��auto��6   $
��Wrap=�6*    �� TextAlignment$ Center=�6>   $��Center=�6U   $	��220��6r   �� 5G         Signup_Message1#�Z 6    �� 5H       �� Command 6   �� *Login+.�67   ��    Signup_Message2#�Z 6;   6l    �� 5I         Signup_Message3#�Z 6   5J      5K      5L      5M      5N      