open System
open System.Linq

let a = Double.Epsilon.CompareTo(Double.MinValue)

let b = Double.NaN.CompareTo(Double.MinValue)
let negativeInfinityComapreToMin = Double.NegativeInfinity.CompareTo(Double.MinValue)
let PositiveInfinityCompareToMinValue = Double.PositiveInfinity.CompareTo(Double.MinValue)

let nanLessMin = Double.NaN < Double.MinValue
let nanLessThanMaxValue = Double.NaN < Double.MaxValue
let positiveInfinityLessMaxValue = Double.PositiveInfinity < Double.MaxValue

let by = Double.NegativeInfinity < Double.MaxValue
let nagavieInfinityLessMin = Double.NegativeInfinity < Double.MinValue

let d = [|Double.NaN; Double.NegativeInfinity|].Min()