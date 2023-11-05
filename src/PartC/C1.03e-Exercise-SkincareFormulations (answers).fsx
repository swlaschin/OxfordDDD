(*
================================================
DDD Exercise: Model skincare formulation

This is a real world example that I worked on!
================================================


A skin care company makes lotions using recipes.
These recipes are called "formulations"

## Formulations

A formulation consists of a list of components,
each of which is an "ingredient" along with a percentage

For example -- "Coconut-scented lotion"
   Lotion:
      Ingredient: Aqua, 45%
      Ingredient: Coconut Oil Mix, 50%
      Ingredient: Vitamin E, 5%


## Ingredients

Each "ingredient" is provided by a supplier.
An ingredient consists of one or more components,
each of which has a chemical name, chemical code (CAS) and percentage of that chemical
in the total ingredient.

Each chemical is uniquely identified by a "CAS" number such as "7732-18-5"

Example Ingredient: Coconut Oil Mix
    Supplied by: ABC
    Component:  Water, 7732-18-5, 50%
    Component:  Sodium Laurel Sulphate, 3088-31-1, 20%
    Component:  Coconut Oil, 8001-31-8, 30%

## INCI

The international chemical name is called an INCI.
CAS and INCI are not 1:1. A CAS number may map to one or more INCIs.

##  Display of ingredients

The list of INCIs in the formation must be shown on the "ingredients" area of the bottle.
The list must be sorted by volume, and allergens shown in bold.

## Exercise: build a domain model for a formulation

References:
INCI: https://en.wikipedia.org/wiki/International_Nomenclature_of_Cosmetic_Ingredients
List of INCIs: https://incibeauty.com/en/ingredients
CAS: https://en.wikipedia.org/wiki/CAS_Registry_Number
List of CAS numbers: https://en.wikipedia.org/wiki/List_of_CAS_numbers_by_chemical_compound

*)


// IMPORTANT: This is NOT a "rec" module, so the types will need to be in order of declaration!

// ===================================
// 1. Define simple basic types
// ===================================

// define types for constrained types un the domain
// E.g. CAS Number, INCI, SupplierId, Percentage
// (dont need to implement the constraints)

type CasNumber = CasNumber of string
type Inci = Inci of string
type SupplierId = SupplierId of string
type Percentage = Percentage of float

// ===================================
// 1. Define Ingredients
// ===================================

type IngredientComponent = {
    Name : string
    CasNumber : CasNumber
    Percent: Percentage
}

type Ingredient = {
    Name : string
    SupplierId : SupplierId
    Components : IngredientComponent list
}

// ===================================
// 2. Define Formulation
// ===================================


type FormulationComponent = {
    Ingredient : Ingredient
    Percent: Percentage
}

type Formulation = {
    Name : string
    Components : FormulationComponent list
}


// ===================================
// 3. Define function types that get the INCIs for the label
// ===================================

// Define a type for a single item on the ingredient label
// It has INCI, percentage for the total formulation, and whether or not an allergen
type IngredientLabelItem = {
    Inci: Inci
    Percent: Percentage
    IsAllergen : bool
}

// Define a type for entire ingredient label, with the items sorted by qty order
type IngredientLabel = {
    FormulationName : string
    Items: IngredientLabelItem list
}

// Define a function to get the ingredient label from a Formulation
type FormulationToIngredientLabel =
    Formulation -> IngredientLabel


// ===================================
// Services
// Such as a lookup in a database
// ===================================


// Define a function to get the list of INCIs from a CAS number
// (This will be a lookup in a database)
type CasToIncis =
    CasNumber -> Inci list

// Define a function to determine if an INCI is an allergen
// (This will be a lookup in a database)
type IsAllergen =
    Inci -> bool


// ===================================
// Extra!
// ===================================


let notImplemented() = failwith "not implemented"

// need a special helper to multiply percentages
let multPercent (Percentage x)(Percentage y) =
    Percentage (x * y)

// dummy implementations for services
let isAllergen : IsAllergen = fun _ -> notImplemented()
let casToIncis : CasToIncis = fun _ -> notImplemented()


// With these definitions, we can implement the functionality of FormulationToIngredientLabel
let formulationToIngredientLabel : FormulationToIngredientLabel =

    fun formulation ->
        formulation.Components

        // multiply all the ingredients by the formulation percentage
        |> List.collect (fun formComp ->
            formComp.Ingredient.Components
            |> List.map (fun ingComp -> {ingComp with Percent = multPercent ingComp.Percent formComp.Percent})
            )
        // after this we now have a list of IngredientComponents, scaled by formulation percentage

        // now convert the CAS numbers to INCIs and convert them to IngredientLabelItems
        |> List.collect (fun (ingComp:IngredientComponent) ->
            ingComp.CasNumber
            |> casToIncis
            |> List.map (fun inci -> {
                Inci=inci
                Percent=ingComp.Percent
                IsAllergen=isAllergen inci
            })
            )

        // now sort the items by percentage
        |> List.sortByDescending (fun ingLabeItem -> ingLabeItem.Percent )

        // now make a IngredientLabel from these items and the formulation name
        |> fun items -> {
            FormulationName = formulation.Name
            Items=items
            }



