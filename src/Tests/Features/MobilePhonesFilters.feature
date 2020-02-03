Feature: MobilePhonesFilters
	This test feature demonstrates how to use this framework

Background: 
	Given I navigate to page 'https://catalog.onliner.by/mobile'
	Then I verify that 'MobilePhonesPage' page is loaded
	And I verify that 'manufacturerPopUpFilter' element is not displayed

Scenario: Use mobile phones filter
	When I set 'xiaomiCheckBox' element selected state to 'true'
	Then I verify that 'xiaomiCheckBox' element is selected
	And I verify that 'sonyCheckBox' element is not selected
	When I type '300' into element 'maxAmountTextBox'
	And I select '5.8"' item from 'screenSizeMaxDropDown' dropdown by 'text' selection type
	And I select '2gb' item from 'ramMinDropDown' dropdown by 'value' selection type
	And I select '18' item from 'screenResolutionMinDropDown' dropdown by 'index' selection type
	Then I verify that 'productDescription' elements contain 'text'