   M S B A M L   `   `   `  ����F  BHCIprojekat, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:HCIprojekatHCIprojekat  5   	       HCIprojekat.DodajTip   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:HCIprojekat     5   	     � Title$  DodajTip��5   	   $	��600��6   $	��800��6'   .��5	      � $	��800��6   $	��600��6   .+�5
   
   �� -   5         ��Name$ labelImeVrste��5
      $��	Ime tipa:��6'   $
��Left=�6;   $��30��6V   $��	15,10,0,0q�6b   $	��Top=�6u   $��53��6�   �� 5   
   -   5         ��Name$ 
textBoxIme��5      $
��Left=�6'   $��25��6B   $��	15,35,0,0q�6N    �� TextWrapping$
 Wrap=�6a   $	��Top=�6u   $	��191��6�    � ToolTip$- 'Unesite ime vrste koju hocete da dodate��6�   �� 5   
   -   5        $ labelOpisVrste��5      $��
Opis tipa:��6(   $
��Left=�6=   $��30��6X   $��	15,65,0,0q�6d   $	��Top=�6w   $	��125��6�   �� 5   
   -   5        $ textBoxOpis��5      $
��Left=�6'   $	��101��6B   $��
15,100,0,0q�6O   $
 Wrap=�6c   $	��Top=�6w   $	��191��6�   $- 'Unesite ime vrste koju hocete da dodate��6�   �� 5   
   -   5        $ label1��5      $��Ikonica:��6    $
��Left=�63   $��
468,42,0,0q�6N   $	��Top=�6b   $��77��6z   �� 5   
   -   5         ��Name$ button��5      $��Browse��6!   $
��Left=�62   $��
550,48,0,0q�6M   $	��Top=�6a   $	��139��6y   $��33��6�   �� 5   
   -   5        $ button1��5      $��Submit��6"   $
��Left=�63   $��373,219,0,0q�6N   $	��Top=�6c   $	��112��6{   $��64��6�   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ dataGrid��5   +   $
��Left=�6=   $��	0,366,0,0q�6X   $	��Top=�6k    J� RenderTransformOrigin$ -2.917,-1.154&�6�   $	��790��6�   $	��202��6�   ��6   �� 6�   �� 5   
   -	   5         ��Name$ image��5      $
��Left=�6   $	��101��6:   $��449,100,0,0q�6G   $	��Top=�6\   $	��322��6t   �� 5   
   -
   5        $ buttonDelete��5      $��Izbrisi��6'   $
��Left=�69   $��236,219,0,0q�6T   $	��Top=�6i   $	��103��6�   $��64��6�   �� 5   
   -   5        $ buttonCancel��5      $��Ponisti��6'   $
��Left=�69   $��
98,219,0,0q�6T   $	��Top=�6h   $	��108��6�   $��64��6�   5      5      