using System;
using Microsoft.FSharp.Core;
namespace ScrapySharp.Core.CSharp
{
	public class TokenContainer
	{
		public TokenContainer()
        {
        }
		public TokenContainer(Token token, int offset) : this()
		{
			this.token = token;
			this.offset = offset;
		}
		public int Offset
		{
			get
			{
				return this.offset;
			}
		}
		public Token Token
		{
			get
			{
				return this.token;
			}
		}
		internal Token token;
		internal int offset;
	}
}