namespace Tests

open NUnit.Framework
open FsUnit
open BalancedBrackets.BalanceChecker

[<TestFixture>]
type BalanceCheckerTest () =
    
    /// Balance brackets test cases
    static member bracketSequences = [
       "", true
       "(", false
       "()", true
       ")(", false
       "(}", false
       "([)]", false
       "([]))", false
       "({[]})", true
       "()[]{}", true
       "({}[(())][])", true
       "(abc)", false
    ]

    [<Test>]
    [<TestCaseSource("bracketSequences")>]
    member this.``balance checker tests`` (testData: string * bool) =
        let (seq, isCorrect) = testData
        seq |> isBracketSequenceCorrect |> should equal isCorrect
