namespace ScrapySharp.Core

    open System
    open System.IO
    open System.Net
    open System.Runtime.Serialization.Formatters.Binary
    open System.Text

    type INavigationProvider<'t> = 
        abstract member ChildNodes : System.Collections.Generic.List<'t> -> System.Collections.Generic.List<'t>
        abstract member Descendants : System.Collections.Generic.List<'t> -> System.Collections.Generic.List<'t>
        abstract member ParentNodes : System.Collections.Generic.List<'t> -> System.Collections.Generic.List<'t>
        abstract member AncestorsAndSelf : System.Collections.Generic.List<'t> -> System.Collections.Generic.List<'t>
        abstract member GetName : 't -> string
        abstract member GetAttributeValue : 't -> string -> string -> string
        abstract member GetId : 't -> string
        abstract member Attributes : 't -> System.Collections.Specialized.NameValueCollection


    type AgilityNavigationProvider() = 
        interface INavigationProvider<HtmlAgilityPack.HtmlNode> with
            member this.ChildNodes(nodes) = 
                let resutls = nodes |> Seq.map (fun x -> x.ChildNodes) |> Seq.collect (fun x -> x)
                new System.Collections.Generic.List<'t>(resutls)
            member this.Descendants(nodes) = 
                let resutls = nodes |> Seq.map (fun x -> x.Descendants()) |> Seq.collect (fun x -> x)
                new System.Collections.Generic.List<'t>(resutls)
            member this.ParentNodes(nodes) = 
                let results = nodes |> Seq.map (fun x -> x.ParentNode)
                new System.Collections.Generic.List<'t>(results)
            member this.AncestorsAndSelf(nodes) = 
                let results = nodes |> Seq.map (fun x -> x.AncestorsAndSelf()) |> Seq.collect (fun x -> x)
                new System.Collections.Generic.List<'t>(results)
            member this.GetName(node) =
                node.Name
            member this.GetAttributeValue node name defaultValue =
                node.GetAttributeValue(name, defaultValue)
            member this.GetId(node) =
                node.Id
            member this.Attributes(node) =
                let attrs = new System.Collections.Specialized.NameValueCollection()
                for attr in node.Attributes do
                    attrs.Add(attr.Name, attr.Value)
                attrs
