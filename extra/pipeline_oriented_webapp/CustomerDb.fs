module CustomerDb

type Customer = {Id: int; Name: string}
    
let loadCustomerFromDb customerId =
    match customerId with
    | 1 -> Ok { Id=1; Name="Scott"}
    | 2 -> Ok { Id=2; Name="Alice"}
    | _ -> Error "customer not found"
