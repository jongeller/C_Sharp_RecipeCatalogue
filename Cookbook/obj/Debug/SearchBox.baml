   M S B A M L   `   `   `  ����  Cookbook    Cookbook.SearchBox   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Recipe Search��5   	   $	��306��6   $	��373��6,   
������� � WindowStartupLocation$ CenterOwner=�6N    � 	Resources 5         RoundCorner �� 
TargetType �� Property �� Value �� 
TargetType �Name ��Name	 �� CornerRadius
 
� GradientOrigin �� RelativeTransform �� CenterX �� CenterY �� ScaleX �� ScaleY T� X T� Y � Offset � Color �� Triggers P� Property P� Value �� 
TargetName��%b  &	        �� 5   
   " ��6$   .�5      ��  � ��$ Center=�6;   �� 5       � ��$ Center=�69   �� 5	       � ��$ 1q�6(   �� 5
       � �� 5      �� 5      " ��6&   .R�5      � $
 grid��6   .+�5      �� $ border��6%   $	 5��65   
�������$��1q�6Y   ��5   "   
� 5   &   $
 0.496,1.052&�6:    5   *   V� 5   .   .~�5   2   �� $	 0.5Q�6A   $	 0.5Q�6O   $	 1.5Q�6]   $	 1.5Q�6j   T� 5   2   $
 0.02Q�6E   $	 0.3Q�6N   5   /   .��5   *   � $ 1Q�67   $ White��6B   � 5   *   $	 0.3Q�67   $ HoneyDew��6D   5   '   ._�5   "   �� $��Center=�63   $��Center=�6P   $��Normal"�6k   6�   5      5       5       P� 5!       � �� . . �5"   "   �� $ border��6?    � �� 5#   &   
� 5$   *   $
 0.496,1.052&�6>    5%   .   V� 5&   2   .~�5'   6   �� $	 0.5Q�6E   $	 0.5Q�6S   $	 1.5Q�6a   $	 1.5Q�6n   T� 5(   6   $
 0.02Q�6I   $	 0.3Q�6R   5)   3   .��5+   .   � $ White��6;   $ 1Q�6I   � 5,   .   $ DarkGray��6;   $	 0.3Q�6L   5-   +   5/   #   50      P� 51       �@J�IsMouseOver . . �52   "   �� $ border��6@    � ��
 �����6T   53      P� 54       � {� .  . �55   "   �� $
 grid��6<    �@J�Opacity$
 0.25Q�6N   56      58      5:      5;      5<      .��5=      �  � RowDefinitions 5>   
   �� 5?      $	��15* �6   $��37��6)   �� 5@      $	��15* �6   $��25��6)   �� 5A      $	��15* �6   �� 5B      $	��40* �6   .+�5E   
   �� -   5         ��Name$ searchLabel��5E      $��0��6%   $��0,0,0,0q�62   $��Search for...��6C   $��Center=�5F      $��Center=�64   $��24(�6V   �� 5G   
   -   5         ��Name$ searchTextBox��5G      $��1��6)   $��5,5,5,5q�66    �� TextWrapping$
 Wrap=�6G   �� 5H   
   -   5         ��Name$ searchButton��5H      $��2��6'   $��0,0,0,0q�64   $��75��6E   $��25��6P   $��Search��6\   #���   5I       �� 	IsDefault . 5J   #   �� 5K   
   $��Options��6   $��28,10,28,10q�6$   $��3��69   $	��109��6F   $	��309��6S   .��5L      ��  �� Orientation$ Vertical=�6   .+�5M      �� $ 
Horizontal=�6   .+�5N      �� $ Vertical=�6!   .+�5O      	� -   5         	�Name$ matchAnyRadioButton��5O   &   $��5,10,0,0q�6C     	� 	GroupName$  MatchOptions��6V   $��Match any of these words��6o   ! d� 	IsChecked$
! TrueY�6�   	� 5P      -   5        $ matchAllRadioButton��5P   &   $��5,5,0,0q�6C   $  MatchOptions��6U   $��Match all of these words��6n   5Q      �� 5R      $ Vertical=�6!   .+�5S      	� -   5        $ searchAllRadioButton��5S   &   $��	10,10,0,0q�6D   $  SearchOptions��6X   $��Search all recipes��6r   $
! TrueY�6�   	� 5T      -   5        $ searchListRadioButton��5T   &   $��10,5,0,0q�6E   $  SearchOptions��6X   $��Search current list��6r   5U      5V      �� 5W      -   5        " ��Name$" caseSensitiveCheckBox��5W      $��0,15,0,0q�6:   $��Case sensitive��6M   $��Center=�6f   5X      5Y      5\      5]      