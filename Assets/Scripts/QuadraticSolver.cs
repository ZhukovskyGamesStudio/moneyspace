using System;
using System.Collections.Generic;

public static class QuadraticSolver {
    public static List<double> SolveEquation(double a, double b, double c) {
        List<double> roots = new List<double>();
        double D = b * b - 4 * a * c;
        switch (D) {
            case < 0:
                return roots;
            case 0:
                roots.Add(-b / (2 * a));
                break;
            default: {
                double sqrtD = Math.Sqrt(D);
                roots.Add((-b + sqrtD) / (2 * a));
                roots.Add((-b - sqrtD) / (2 * a));
                break;
            }
        }

        return roots;
    }
}