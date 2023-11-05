module rec CrcCardDemo =
// =================================
// CRC Demo
// =================================

    // aliases
    type DateTime = System.DateTime
    type ProductId = string

(*
Documentation from listening to domain experts:

* A Customer has a name, address, and customer number
* A Customer can place an Order
* An Order has a placement date, a delivery date, a total amount, etc
* An Order consists of a list of OrderItems
* An OrderItem has a product, qty, etc
*)

    type Customer =
        // getter and setter for Name
        abstract member Name : string
        abstract member SetName : string -> unit
        // etc

        abstract member PlaceOrder : Order -> unit

    type Order =
        // getter for PlacementDate
        abstract member PlacementDate : DateTime
        // NOTE: no public setter. It is set at creation time

        // getter for OrderItems
        abstract member Items : OrderItem list

    type OrderItem =
        // getter for ProductId
        abstract member ProductId : ProductId

        // reference to parent
        abstract member Order : Order


(*
Questions:

* Who should be responsible for placing an Order?
    Customer.PlaceOrder(order)
    Order.CreateWithCustomer(customer)
    OrderManagementSystem.PlaceOrder(order,customer)

* If you wanted to test that the PlacementDate was set properly
  at creation time, how would you?

* Should the OrderItem have a reference to the parent Order?

*)