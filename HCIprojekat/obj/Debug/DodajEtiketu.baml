   M S B A M L   `   `   `  ����F  BHCIprojekat, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:HCIprojekatHCIprojekat  5   	       HCIprojekat.DodajEtiketu   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   X TXceed.Wpf.Toolkit, Version=3.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5   	   Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5   	   %localclr-namespace:HCIprojekat  5   	     � Title$  DodajEtiketu��5	   	   $	��467��6   $	��300��6+   .��5
      � $��0,0,38,0q�6   .+�5   
   �� -   5         ��Name$ labelIdVrste��5      $��Boja:��6&   $
��Left=�66   $��30��6Q   $��	20,30,0,0q�6]   $	��Top=�6p   $��46��6�   �� 5   
   -   5        $ labelOpisVrste��5      $��Naziv etikete:��6(   $
��Left=�6A   $��30��6\   $��	15,95,0,0q�6h   $	��Top=�6{   $	��108��6�   �� 5   
   -   5         ��Name$ textBoxNaziv��5      $
��Left=�6(   $��50��6C   $��
15,130,0,0q�6O    �� TextWrapping$
 Wrap=�6c   $	��Top=�6w   $	��145��6�    � ToolTip$- 'Unesite ime vrste koju hocete da dodate��6�   �� 5   
   -   5         ��Name$ button1��5      $��Submit��6"   $
��Left=�63   $��
10,225,0,0q�6N   $	��Top=�6b   $��65��6z   6�   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ dataGrid��5      $
��Left=�6%   $��0,275,-36,0q�6@   $	��Top=�6U   $	��161��6m   $	��290��6z   6�   �� 5   
   -   5        $ buttonDelete��5      $��Izbrisi��6'   $
��Left=�69   $��100,225,0,0q�6T   $	��Top=�6i   $��75��6�   6�   �� 5   
   -   5        $ buttonCancel��5      $��Ponisti��6'   $
��Left=�69   $��200,225,-21,0q�6T   $	��Top=�6k   $��75��6�   6�   #  Xceed.Wpf.Toolkit.ColorPicker  5   
   -   5          Name$ 	bojaBirac��5         SelectedColor$ 	#FF0F27C1X�60   $��15,60,113,351q�6J   5      5      