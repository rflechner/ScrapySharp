namespace ScrapySharp.Core

    type INavigationProvider<'t> = 
        abstract member ChildNodes : System.Collections.Generic.IEnumerable<'t> -> System.Collections.Generic.IEnumerable<'t>
        abstract member Descendants : System.Collections.Generic.IEnumerable<'t> -> System.Collections.Generic.IEnumerable<'t>
        abstract member ParentNodes : System.Collections.Generic.IEnumerable<'t> -> System.Collections.Generic.IEnumerable<'t>
        abstract member AncestorsAndSelf : System.Collections.Generic.IEnumerable<'t> -> System.Collections.Generic.IEnumerable<'t>
        abstract member GetName : 't -> string
        abstract member GetAttributeValue : 't -> string -> string -> string
        abstract member GetId : 't -> string
        abstract member Attributes : 't -> System.Collections.Specialized.NameValueCollection

    type AgilityNavigationProvider() = 
        interface INavigationProvider<HtmlAgilityPack.HtmlNode> with
            member this.ChildNodes(nodes) = 
                nodes
                |> Seq.map (fun x -> x.ChildNodes)
                |> Seq.collect id
            member this.Descendants(nodes) = 
                nodes
                |> Seq.map (fun x -> x.Descendants())
                |> Seq.collect id
            member this.ParentNodes(nodes) = 
                nodes
                |> Seq.map (fun x -> x.ParentNode)
            member this.AncestorsAndSelf(nodes) = 
                nodes
                |> Seq.map (fun x -> x.AncestorsAndSelf())
                |> Seq.collect id
            member this.GetName(node) =
                node.Name
            member this.GetAttributeValue node name defaultValue =
                node.GetAttributeValue(name, defaultValue)
            member this.GetId(node) =
                node.Id
            member this.Attributes(node) =
                let attrs = System.Collections.Specialized.NameValueCollection()
                for attr in node.Attributes do
                    attrs.Add(attr.Name, attr.Value)
                attrs
