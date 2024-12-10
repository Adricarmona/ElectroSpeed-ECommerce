import { BiciTemporal } from "./bici-temporal";

export interface OrdenTemporal {
    idUsuario: number,
    Bici: BiciTemporal[],
}