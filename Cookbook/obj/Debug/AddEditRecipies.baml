   M S B A M L   `   `   `  ����C  ?Cookbook, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:CookbookCookbook  5   	       Cookbook.AddEditRecipies   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:Cookbook     5   	   $	��430��5   	   $	��800��6   $	��300��6"   $	��600��62   
�������  � WindowStartupLocation$  CenterOwner=�6W    � 	Resources 5
         RoundCorner �� 
TargetType �� Property �� Value �� 
TargetType �Name ��Name �� CornerRadius	 
� GradientOrigin
 �� RelativeTransform �� CenterX �� CenterY �� ScaleX �� ScaleY T� X T� Y � Offset � Color �� Triggers P� Property P� Value �� 
TargetName��%b  &	        �� 5   
   " ��6$   .�5      ��  � ��$ Center=�6;   �� 5       � ��$ Center=�69   �� 5       � ��$ 1q�6(   �� 5       � �� 5      �� 5      " ��6&   .R�5      � $
 grid��6   .+�5      �� $ border��6%   $ 5��65   
�������$��1q�6Y   ��5   "   
� 5   &   $	 0.496,1.052&�6:   
 5   *   V� 5   .   .~�5   2   �� $	 0.5Q�6A   $	 0.5Q�6O   $	 1.5Q�6]   $	 1.5Q�6j   T� 5   2   $
 0.02Q�6E   $	 0.3Q�6N   5   /   .��5   *   � $ 1Q�67   $ White��6B   � 5   *   $	 0.3Q�67   $ HoneyDew��6D   5   '   ._�5!   "   �� $��Center=�63   $��Center=�6P   $��Normal"�6k   6�   5"      5#       5%      P� 5&       � �� . . �5'   "   �� $ border��6?    � �� 5(   &   
� 5)   *   $	 0.496,1.052&�6>   
 5*   .   V� 5+   2   .~�5,   6   �� $	 0.5Q�6E   $	 0.5Q�6S   $	 1.5Q�6a   $	 1.5Q�6n   T� 5-   6   $
 0.02Q�6I   $	 0.3Q�6R   5.   3   .��50   .   � $ White��6;   $ 1Q�6I   � 51   .   $ DarkGray��6;   $	 0.3Q�6L   52   +   54   #   55      P� 56       �@J�IsMouseOver . . �57   "   �� $ border��6@    � ��
 �����6T   58      P� 59       � {� .  . �5:   "   �� $
 grid��6<    �@J�Opacity$
 0.25Q�6N   5;      5=      5?      5@      5A      .��5B      �  � ColumnDefinitions 5D   
   �� 5E      $	��10* �6   $��75��6+   �� 5F      $	��10* �6   $��75��6+   �� 5G      $	��10* �6   $��75��6+   �� 5H      $	��30* �6   $��75��6+   �� 5I      $	��20* �6   $	��150��6+   �� 5J      $	��20* �6   $	��150��6+    � RowDefinitions 5M   
   �� 5N      $��6* �6   �� 5O      $��6* �6   �� 5P      $��6* �6   �� 5Q      $��6* �6   �� 5R      $��5* �6   �� 5S      $	��17* �6   �� 5T      $��5* �6   �� 5U      $	��17* �6   �� 5V      -   5         ��Name$ testRow��5V      $��6* �6-   .+�5Z   
   �� -   5         ��Name$ lblRecipeTitle��5Z      $��Recipe Title��6(   $��5,0,0,-5q�6?   $��70��6Q   $��0��5[      $��0��6    $
��Left=�5\      $��Center=�6+   �� 5^   
   -   5         ��Name$ tbRecipeTitle��5^      $��5,0,5,0q�6)   $��23��6:   $��1��5_      $��0��6"   $��3��6/   $��Center=�5`       �� TextWrapping$
 Wrap=�5a      ��6&   �� *RecipeTitle+�� 5c   
   -   5        $ lblRecipeType��5c      $��Recipe Type��6'   $��5,0,0,-5q�6=   $��0��5d      $��1��6    $
��Left=�5e      $��Center=�6,   �� 5g   
   -   5        $ tbRecipeType��5g      $��5,0,5,0q�6(   $��23��69   $��1��5h      $��1��6    $
 Wrap=�5i      ��6$   �� *
RecipeType+�� 5k   
   -   5        $ lblRecipeYield��5k      $��Recipe Yield��6(   $��5,0,0,-5q�6?   $��0��5l      $��2��6    $
��Left=�5m      $��Center=�6+   �� 5o   
   -   5        $ tbRecipeYield��5o      $��5,0,5,0q�6)   $��23��6;   $��1��5p      $��2��6"   $��2��6/   $��Center=�5q      $
 Wrap=�5r      ��6&   �� *RecipeYield+�� 5t   
   -   5        $ lblRecipeServingSize��5t      $��Serving Size��6.   $��5,0,0,-5q�6E   $��2��5u      $��1��6    $
��Left=�5v      $��Center=�6+   �� 5x   
   -	   5        $ tbRecipeServingSize��5x      $��	5,5,5,-10q�6/   $��3��5y      $��1��6    $��3��6-   $
 Wrap=�5z       �� AcceptsReturn . ��.  $
��Auto=�5{   6   ��5|      �� *RecipeServingSize+�� 5~   
   -
   5        $ lblDirections��5~      $��
Directions��6'   $��5,0,0,-5q�6<   $��0��5      $��4��6    $
��Left=�5�      $��Center=�6+   �� 5�   
   -   5        $ tbDirections��5�      $��5,0,5,0q�6(   $��0��5�      $��5��6"   $��4��6/   $��Stretch=�5�      $
 Wrap=�6.    . ��.  $
��Auto=�5�   8   ��5�      �� *RecipeDirections+�� 5�   
   -   5        $ 
lblComment��5�      $��Comments��6$   $��5,0,0,-5q�67   $��0��5�      $��6��6    $
��Left=�5�      $��Center=�6+   �� 5�   
   -   5        $ 	tbComment��5�      $��5,0,5,0q�6%   $��0��5�      $��7��6"   $��4��6/   $��Stretch=�5�      $
 Wrap=�6.    . ��.  $
��Auto=�5�   8   ��5�      �� *RecipeComment+�� 5�   
   -   5         ��Name$ 	btnSubmit��5�      $��Submit��6$   $��5,0,0,0q�65   $��55��6G   $��0��5�      $��8��6!   $
��Left=�5�      $��Center=�6,   #���   5�      5�      �� 5�   
   -   5        $ 	btnCancel��5�      $��Cancel��6$   $��5,0,0,0q�65   $��55��6G   $��1��5�      $��8��6!   $��Right=�5�      $��Center=�6-   #���   5�      5�      �� 5�   
   -   5        $ lblIngredients��5�      $��Ingredients��6(   $��5,0,0,-5q�6>   $��4��5�      $��0��6    $
��Left=�5�      $��Center=�6+   �� 5�   
   -   5        $ btnAddIngredient��5�      $	��Add��6+   $��0,0,5,0q�6:   $��45��6L   $��4��5�      $��0��6!   $��Right=�5�      $��Center=�6-   #���   5�      5�      �� 5�   
   -   5        $ btnEditIngredient��5�      $
��Edit��6,   $��5,0,0,0q�6;   $��45��6M   $��5��5�      $��0��6!   $
��Left=�5�      $��Center=�6,   {�.  #���   5�      5�      �� 5�   
   -   5        $ btnDeleteIngredient��5�      $��Delete��6.   $��	56,7,57,7q�6?   $��45��6S   $��5��5�      $��0��6!   $��Center=�5�      $��Center=�6.   {�.  #���   5�      5�      �� 5�   
   -   5        $ btnSubmitIngredient��5�      $��Submit��6.   $��0,0,5,0q�6?   $��45��6Q   $��5��5�      $��0��6!   $��Right=�5�      $��Center=�6-   {�.  #���   5�      5�      �� 5�   
   -   5        $ tbIngredient��5�      $��5,5,5,0q�6(   $��23��69   $��4��5�      $��1��6"   $��2��6/   $	��Top=�5�      $
 Wrap=�5�      {�.  6&   �� 5�   
   -   5         ��Name$ lstbxIngredients��5�      $��5,0,5,0q�6,   $��4��5�      $��2��6"   $��2��6/   $��6��6C   ��5�      �� *RecipeIngredients+5�      �� 5�   
   -   5        $ lblValidation��5�      $��
Validation��6'   $��5,0,5,0q�6<   $��2��5�      $��8��6    $��2��6-   $��Center=�5�      $��Center=�6+   
���  ��$y�Hidden=�5�   !   5�      5�      