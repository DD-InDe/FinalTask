   M S B A M L   `   `   `  ����B  >Desktop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:Desktop.PagesDesktop.Pages  5      #    Desktop.Pages.ConstructorPage   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -localclr-namespace:Desktop.Pages     5      -   5          N� Title$  Конструктор��5	       � 	Resources 5         ModuleToolTip �� Orientation��%F  &	        _� 5   
   ��5      �� 5      $ Vertical=�6   .+�5      �� $ 
Horizontal=�6!   .+�5      �� $&�� Кодовое название:��6$   �� 5      ��6$   �� *
CodeName+5      �� 5      $ 
Horizontal=�6!   .+�5      �� $3��-Срок прохождения модуля:��6$   �� 5      ��6$   �� *ModuleCompleteDeadline+5      �� 5      $ 
Horizontal=�6!   .+�5      �� $*��$Ответственное лицо:��6$   �� 5      ��6$   �� *ResponsiblePerson.FullName+5      5      5      5       .��5"      �  � ColumnDefinitions 5#   
   �� 5$      $��* �6   �� 5%      $��* �6   .+�5(   
   ��   Sample-Border#��[ 6   ._�5)      �� $ Vertical=�6   .+�5*      �� $+��%Программа адаптации��6     Header-Text#��[ 5+      �� 5-        	Main-Text#��[ 6   $��Сотрудник:��5.      �� 5/      -   5         ��Name$ EmployeeComboBox��5/        Main-ComboBox#��[ 50       �� SelectedValuePath$ Id��51       �� DisplayMemberPath$ FullName��53       �� SelectedValue 52      �� *
EmployeeId+53      �� 54      #��[ 6   $��Отдел:��55      �� 56      -   5        $ DepartmentComboBox��56      #��[ 57      $ Id��58      $
 Name��5:       59      �� *DepartmentId+5:      �� 5;      #��[ 6   $��Должность:��5<      �� 5=      -   5        $ PositionComboBox��5=      #��[ 5>      $ Id��5?      $
 Name��5B       5@      �� *
PositionId+5B      �� 5D      -   5         ��Name$ 
SaveButton��5D        Main-Button#��[ 5F      $��Сохранить��5G      5H      5I      �� 5J   
   #��[ 6   $��1��5K      ._�5L      �� $ Vertical=�6   .+�5M      �� $��Модули��6   #��[ 5N      �� 5O      -   5        	 ��Name$	 ModuleItems��5O      ��5P      �� 5Q      .R�5R      �� $ 
Horizontal=�6)   .+�5S   "   d�   Main-ToggleButton#��[ 6/   
 � ToolTip#
 [  5T   /   $��Stretch=�5U   /    d� 	IsChecked 5V   /   �� *
IsSelected+��5W   /   �� *Module.Name+�� 5X   "   $ EmployeeComboBox��6+   $ FullName��5Y   +   #��[ 5\   +   ��5Z   +   �� *	Employees+ �� SelectedItem 5[   +   �� *
Employee+5\   +   5]      5^      5`      5a      5b      5c      5d      