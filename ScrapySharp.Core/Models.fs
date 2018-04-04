namespace ScrapySharp.Core

    open System
    open System.IO
    open System.Net
    open System.Runtime.Serialization.Formatters.Binary
    open System.Text

    type Token =
        | ClassPrefix of int
        | IdPrefix of int
        | TagName of int * string
        | CssClass of int * string
        | CssId of int * string
        | AllChildren of int
        | OpenAttribute of int 
        | CloseAttribute of int
        | AttributeName of int * string
        | AttributeValue of int * string
        | Assign of int
        | EndWith of int
        | StartWith of int
        | DirectChildren of int 
        | Ancestor of int
        | AttributeContainsPrefix of int
        | AttributeContains of int
        | AttributeContainsWord of int
        | AttributeNotEqual of int
        | Checkbox of int
        | Checked of int
        | Disabled of int
        | Enabled of int
        | Selected of int


    type TokenContainer(token:Token, offset:int) =
        member t.Offset = offset
        member t.Token = token

    type CharContainer(c:char, offset:int) =
        member t.Offset = offset
        member t.Char = c