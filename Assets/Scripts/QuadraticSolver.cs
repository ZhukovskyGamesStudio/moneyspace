using System;

public static class QuadraticSolver {
    public static bool SolveEquation(double a, double b, double c, out double maxRoot) {
        double D = b * b - 4 * a * c;
        switch (D) {
            case < 0:
                maxRoot = -1;
                return false;
            case 0:
                maxRoot = -b / (2 * a);
                return true;
            default: {
                double sqrtD = Math.Sqrt(D);
                maxRoot = Math.Max((-b + sqrtD) / (2 * a), (-b - sqrtD) / (2 * a));
                return true;
            }
        }
    }
}