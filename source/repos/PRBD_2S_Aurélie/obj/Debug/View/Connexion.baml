   M S B A M L   `   `   `  ����I  EPRBD_Framework, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG4clr-namespace:PRBD_Framework;assembly=PRBD_FrameworkPRBD_Framework  5   	   K GPRBD_2S_Aurélie, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:PRBD_2S_Aurélie.PropertiesPRBD_2S_Aurélie.Properties 5   	   3clr-namespace:PRBD_2S_AuréliePRBD_2S_Aurélie 5	   	       PRBD_2S_Aurélie.Connexion   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Bf4clr-namespace:PRBD_Framework;assembly=PRBD_Framework     5   	   7p)clr-namespace:PRBD_2S_Aurélie.Properties     5   	   0localclr-namespace:PRBD_2S_Aurélie     5	   	   _iQclr-namespcace:System.Windows.Interactivity;assembly=System.Windows.Interactivity     5
   	   -   5           PRBD_Framework.WindowBase   Name$  window��5   	   +  %PRBD_2S_Aurélie.Properties.Resources   Connexion_Title � Title# Z 5   	    � WindowStartupLocation$ CenterScreen=�68    � 
ResizeMode$ NoResize=�5   I   $	��700��5   	   $	��400��6    � SizeToContent$ WidthAndHeight=�5   	    +� FocusedElement 5   	   ��  �� ElementName$ txtUserName��.��5      � $��10q�6    � ColumnDefinitions 5   
   �� 5      $
��auto �6   �� 5      $��2* �6   	 � RowDefinitions	 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� -   5        
 ��Name$
 label��5         Connexion_UserName#��Z 6   $��Center=�6S   $��5q�6n   #   PRBD_Framework.ValidatedField  5   
   $��1��6   $��Center=�6+   .+�5      �� -   5         ��Name$ txtUserName��5       �� TextWrapping$
 Wrap=�6+   $��1��6?   ��5      �� *
UserName+ �� UpdateSourceTrigger$ PropertyChanged=�5       �� 5!   
      Connexion_Password#��Z 6   $��1��6D   $��Center=�6Q   $��5q�6l     5"   
   $��1��6   $��1��6(   $��Center=�68   .+�5#      G� -   5         G�Name$ txtPassword��5#      #   PRBD_Framework.PasswordHelper   Attach . $��1��5&      $��1��6#   $��Center=�63      Password 5%      �� *
Password+$ PropertyChanged=�5&   3   5'      ]� 5(   
   $��2��6   $��2��6!   $��Center=�65   $��Center=�6P   $��10q�6m   .+�5)      �� -   5         ��Name$ 	btnCancel��5)         Connexion_Annuler#��Z 6(   $
��Left=�5*      $	��Top=�60   $��75��6H    �� IsCancel . $��0,0,10,0q�6c   ��5+      �� *	Annuler+�� 5,      -   5        $ btnLogin��5,      #��Z 6'   $
��Left=�5-      $	��Top=�60   $��75��6H    �� 	IsDefault . $��10,0,0,0q�6d   ��5.      �� *Login+5/      ]� 50   
   $��3��6   $��2��6!   .+�51      �� $
��auto��6   $
��Wrap=�6&    �� TextAlignment$ Center=�6:   $��Center=�6Q   $	��220��6n   �� 52         Connexion_Message_1#�Z 6    �� 53       �� Command 6   �� *Inscription+.�6=   ��    Connexion_Message_2#�Z 6A   6v    �� 54         Connexion_Message_3#�Z 6   55      56      57      58      