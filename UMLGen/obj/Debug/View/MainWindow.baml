   M S B A M L   `   `   `  ����A  =UMLGen, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null+clr-namespace:UMLGen.ModelUMLGen.Model  5   	   )clr-namespace:UMLGen.ViewUMLGen.View  5      c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity 5	   	   f bGalaSoft.MvvmLight.Platform, Version=5.2.0.37226, Culture=neutral, PublicKeyToken=5f873c45e98af8a1lMclr-namespace:GalaSoft.MvvmLight.Command;assembly=GalaSoft.MvvmLight.PlatformGalaSoft.MvvmLight.Command 5
   	       UMLGen.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0ignorehttp://www.galasoft.ch/ignore     5   	   ,Modelclr-namespace:UMLGen.Model     5   	   *Viewclr-namespace:UMLGen.View     5      ^iPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity     5	   	   ]cmdMclr-namespace:GalaSoft.MvvmLight.Command;assembly=GalaSoft.MvvmLight.Platform     5
   	   $	��600��5   	   $	��800��5   	     � Title$  UMLGen��5   	    � DataContext 5   	   �� *Main+   Locator �� Source# [  .��5      (�  (� Stretch$ Uniform=�6   .��5   
   ]� $
��Left=�6   ��. $	��790��6D   .+�5          UMLGen.View.CustomListView  -   5          Name$ SideBar��5      o� 5      -   5         o�Name$
 menu��5      $	��Top=�6
   $	��Top=�6-   $��
-100,0,0,0q�6E   .@�5      m� $��_File��6   $��30��6*   $��70��66   $��10,0,0,0q�6A   .@�5      m� $
��_New��6    m� Command 6-   �� *NewDiagramCommand+m� 5       $��_Open��6    6.   �� *LoadDiagramCommand+m� 5!      $��_Save��6    6.   �� *SaveCurrentCommand+m� 5"      $��_Save as��6    61   �� *SaveCurrentAsCommand+5#      m� 5$      $��_Edit��6   $��30��6*   $��70��66   $��10,0,0,0q�6A   .@�5%      m� $��_Copy��6    6.   �� *CopyShapeCommand+m� 5&      $
��_Cut��6    6-   �� *CutShapeCommand+m� 5'      $��_Paste��6    6/   �� *PasteShapeCommand+m� 5(      $��_Undo��6    6.   �� *UndoCommand+m� 5)      $��_Redo��6    6.   �� *RedoCommand+m� 5*      $��_Delete��6    61   �� *RemoveShapeCommand+5+      5,      c� 5-      -   5         c�Name$ toolBar1��5-   +   $	��Top=�6   $��40��6=   $��
-100,0,0,0q�6I    `� IsLocked . 	 c� OverflowMode$	 Never=�6y   $
��Auto��6�   .@�5.      �� -   5        
 ��Name$
 CopyBtn��5.      $��30��6*    � ToolTip$! Copy selection to clipboard��6[   $��Center=�6�   $��75��6�   ��66   �� *CopyShapeCommand+.��5/      �� $��/Images/Copy.png��6   50      �� 51      -   5        $
 CutBtn��51      $��30��6)   $  Cut selection to clipboard��6[   $��Center=�6�   $��75��6�   ��65   �� *CutShapeCommand+.��52      �� $��/Images/Cut.png��6   53      �� 54      -   5        $
 PasteBtn��54      $��30��6+   $) #Pastes the element in the clipboard��6^   $��Center=�6�   $��75��6�   ��67   �� *PasteShapeCommand+.��55      �� $��/Images/Paste.png��6   56      �� 57      -   5        $
 AddUML��57      $��30��6)   $ Adds a new UML element��6X   $��Center=�6y   $��75��6�   ��65   �� *AddUMLCommand+.��58      �� $��/Images/Plus.png��6   59      �� 5:      -   5        $
 	AddSquare��5:      $��30��6,   $ Adds a new square��6^   $��Center=�6z   $��75��6�   ��68   �� *AddSquareCommand+.��5;      �� $��/Images/Plus.png��6   5<      �� 5=      -	   5        $
 	AddCircle��5=      $��30��6,   $ Adds a new circle��6]   $��Center=�6y   $��75��6�   ��68   �� *AddElipseCommand+.��5>      �� $��/Images/Plus.png��6   5?      5@      �� 5B      $��Bottom=�6   $��20��60   $	��Top=�6<   $��Center=�6T   $
��Auto��6q   ��5C      �� 5D      .R�5E      �  � ColumnDefinitions 5F      �� 5G   "   $	��100 �63   �� 5H   "   $
��Auto �63   �� 5I   "   $��* �63   �� 5J   "   $
��Auto �63   �� 5K   "   $	��100 �63   5M      5N      .@�5P      �� $��1��6    .��5Q      �� $��	TODO:::::��6    5R      5S      �� 5U      $
��Auto=�6   $
��Auto=�6>   .��5V      �� ��6   �� *Shapes+��5W      �� 5X      .R�5Z      �� $	��500��6%   $	��500��62   
������� J� 	AllowDrop . .  (System.Windows.Interactivity.Interaction   Triggers 5[   "   /  )System.Windows.Interactivity.EventTrigger  5\   &      	EventName$
 Drop��65   .  (System.Windows.Interactivity.TriggerBase   Actions. 5]   *   /  )GalaSoft.MvvmLight.Command.EventToCommand     PassEventArgsToCommand .    Command 6=   �� *DataContext.DdDropCommand+ �� RelativeSource �� *FindAncestor+ �� AncestorType" ��6�   5^   '     5_   &   $ 	DragEnter��65   . 5`   *      .  6=   �� * DataContext.DdDragEnterCommand+ �� *FindAncestor+" ��6�   5a   '     5b   &   $ 	DragLeave��65   . 5c   *      .  6=   �� *DataContext.DdDragExitCommand+ �� *FindAncestor+" ��6�   5d   '     5e   &   $ DragOver��65   . 5f   *      .  6=   �� *DataContext.DdDragOverCommand+ �� *FindAncestor+" ��6�   5g   '   5i      5k      ��5m      �� 5n       �� 
TargetType" ��6    .�5o      ��  �� Property �@��Left �� Value 6<   �� *X+�� 5p       �@��Top 6;   �� *Y+5q      5s      5t      5v      5w       J� InputBindings 5z      �� 5{   
    �� 	Modifiers$
 Ctrlf�6   
 �� Key$ Z��6&    �� Command 6.   �� *UndoCommand+�� 5|   
   $
 Ctrlf�6   $ Y��6&    6.   �� *RedoCommand+�� 5}   
   $	 Esc��6    6   �� *DeselectShapeCommand+�� 5~   
   $ 	Backspace��6    6%   �� *RemoveShapeCommand+�� 5   
   $ Delete��6    6"   �� *RemoveShapeCommand+�� 5�   
   $
 Ctrlf�6   $ X��6&    6.   �� *CutShapeCommand+�� 5�   
   $
 Ctrlf�6   $ C��6&    6.   �� *CopyShapeCommand+�� 5�   
   $
 Ctrlf�6   $ V��6&    6.   �� *PasteShapeCommand+�� 5�   
   $
 Ctrlf�6   $ S��6&    6.   �� *SaveCurrentCommand+�� 5�   
   $	 F12��6    6   �� *SaveCurrentAsCommand+�� 5�   
   $
 Ctrlf�6   $ O��6&    6.   �� *LoadDiagramCommand+�� 5�   
   $
 Ctrlf�6   $ N��6&    6.   �� *NewDiagramCommand+5�      