   M S B A M L   `   `   `  ����c  _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity  5      f bGalaSoft.MvvmLight.Platform, Version=5.2.0.37226, Culture=neutral, PublicKeyToken=5f873c45e98af8a1lMclr-namespace:GalaSoft.MvvmLight.Command;assembly=GalaSoft.MvvmLight.PlatformGalaSoft.MvvmLight.Command 5      A =UMLGen, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null)clr-namespace:UMLGen.ViewUMLGen.View 5         UMLGen.View.UMLClass   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ^iPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity     5      ]cmdMclr-namespace:GalaSoft.MvvmLight.Command;assembly=GalaSoft.MvvmLight.Platform     5      +localclr-namespace:UMLGen.View     5        �� Left  5
      �� *X+
 �� Top 6(   �� *Y+��6A   �� *Width+.��5      �  � RowDefinitions 5   
   �� 5      $��4 �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   (� $��1��6   $��50��6   $
��Left=�6.   $	��Top=�6I   .��5      � .+�5      �� -   5         ��Name$ 	ClassName��5       �� TextWrapping$
 Wrap=�6-    �� AcceptsReturn . ��6A   �� *	ClassName+6\   �� 5      
����� �$��Center=�6)   $��5��6F   $��0,0,0,0q�6Q   
���   �$	��Top=�6q   $��5��6�   .   (System.Windows.Interactivity.Interaction   Triggers 5      /   )System.Windows.Interactivity.EventTrigger  5         	EventName$ 	MouseDown��6)   .   (System.Windows.Interactivity.TriggerBase   Actions. 5      /  )GalaSoft.MvvmLight.Command.EventToCommand  	   PassEventArgsToCommand	 . 
   Command
 61   �� *&$DataContext.MouseDownArrowTopCommand+ �� RelativeSource �� *FindAncestor+ �� AncestorType" �6�   5      5      5      5       (� 5"   
   $��2��6   $	��200��6   $
��Left=�6/   $	��Top=�6J   .��5#      �� -   5        $ 	FieldsBox��5#      $
 Wrap=�6)    . ��6=   �� *
FieldNames+6Y   5$      (� 5&   
   $��3��6   $	��100��6   $
��Left=�6/   $	��Top=�6J   .��5'      �� -   5        $ 	MethodBox��5'      $
 Wrap=�6*    . ��6>   �� *MethodNames+6[   5(   
   �� 5*   
   
���   �$��2q�6%    5+        5,      $ 	MouseDown��6!   . 5-        	 . 
 6)   �� *#!DataContext.MouseDownShapeCommand+ �� *FindAncestor+" �6�   5.        5/      $ 	MouseMove��6!   . 50        	 . 
 6)   �� *#!DataContext.MouseMoveShapeCommand+ �� *FindAncestor+" �6�   51        52      $ MouseUp��6!   . 53        	 . 
 6)   �� *!DataContext.MouseUpShapeCommand+ �� *FindAncestor+" �6�   54      56      �� 57   
   
���   �$��2q�6%   $��0��69    58        59      $ 	MouseDown��6!   . 5:        	 . 
 6)   �� *#!DataContext.MouseDownShapeCommand+ �� *FindAncestor+" �6�   5;        5<      $ 	MouseMove��6!   . 5=        	 . 
 6)   �� *#!DataContext.MouseMoveShapeCommand+ �� *FindAncestor+" �6�   5>        5?      $ MouseUp��6!   . 5@        	 . 
 6)   �� *!DataContext.MouseUpShapeCommand+ �� *FindAncestor+" �6�   5A      5D      �� 5E   
   
���   �$��1q�6%   $��1��69   5G      �� 5H   
   
���   �$��1q�6%   $��2��69   ._�5I      � .+�5J      �� 
����� �$
��Left=�6%   $��10��6@   $��0,0,0,0q�6L   
���   �$��Center=�6l   $��10��6�    5K        5L      $ 	MouseDown��6)   . 5M        	 . 
 61   �� *'%DataContext.MouseDownArrowLeftCommand+ �� *FindAncestor+" �6�   5N      5P      �� 5R      
����� �$��Right=�6%   $��10��6A   $��0,0,0,0q�6M   
���   �$��Center=�6m   $��10��6�    5S        5T      $ 	MouseDown��6)   . 5U        	 . 
 61   �� *(&DataContext.MouseDownArrowRightCommand+ �� *FindAncestor+" �6�   5V      5X      5Y      5Z      �� 5\   
   
���   �$��1q�6%   $��3��69   ._�5^      �� 
����� �$��Center=�6%   $��10��6B   $��0,0,0,0q�6N   
���   �$��Bottom=�6n   $��10��6�    5_        5`      $ 	MouseDown��6%   . 5a        	 . 
 6-   �� *&$DataContext.MouseDownArrowBotCommand+ �� *FindAncestor+" �6�   5b      5d      5e      5h      5j      