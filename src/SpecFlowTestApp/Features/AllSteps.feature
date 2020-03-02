@ignoreFeature
Feature: AllSteps
	The scenario contains the list of all implemented steps

#@ignoreScenario
@ignore
Scenario: All step types example
#======== Actions ==============
	Given I navigate to page 'Url'
	When I click on 'WebElement' element
	When I move cursor to 'WebElement' element
	When I set 'WebElement' element selected state to 'true OR false'
	When I type 'text' into element 'WebElement'
	When I select 'text' item from 'DropDown' dropdown by 'text' selection type
	When I select 'value' item from 'DropDown' dropdown by 'value' selection type
	When I select 'index' item from 'DropDown' dropdown by 'index' selection type
#======== Verifications ========
	Then I verify that 'PageName' page is loaded
	Then I verify 'WebElement' element is exist
	Then I verify 'WebElement' element is not exist
	Then I verify that 'WebElement' element is displayed
	Then I verify that elements are displayed:
		| element1    | element2    |
		| WebElement1 | WebElement2 |
	Then I verify that 'WebElement' element is not displayed
	Then I verify that elements are not displayed:
		| element1    | element2    |
		| WebElement1 | WebElement2 |
	Then I verify that 'WebElement' element is enabled
	Then I verify that elements are enabled:
		| element1    | element2    |
		| WebElement1 | WebElement2 |
	Then I verify that 'WebElement' element is not enabled
	Then I verify that elements are not enabled:
		| element1    | element2    |
		| WebElement1 | WebElement2 |
	Then I verify that 'WebElement' element is selected
	Then I verify that elements are selected:
		| element1    | element2    |
		| WebElement1 | WebElement2 |
	Then I verify that 'WebElement' element is not selected
	Then I verify that elements are not selected:
		| element1    | element2    |
		| WebElement1 | WebElement2 |

# Need to implement
# verify element\elements text equal
# verify element\elements text contain
# verify element\elements attribute equal
# verify element\elements attribute contain