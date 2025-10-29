void BrushUV_float(float2 uv, float Density, float2 Tiles, float RotJitter,
                   float ScaleMin, float ScaleMax, out float2 atlasUV)
{
    // Escala de celdas
    float2 uvS    = uv * Density;
    float2 cell   = floor(uvS);
    float2 cellUV = frac(uvS);

    // Hash pseudo-aleatorio por celda (barato y estable)
    float3 r = frac(sin(float3(cell, cell.x+cell.y) * 127.1) * 43758.5453);

    // Selección de tile en el atlas
    float tilesCount = Tiles.x * Tiles.y;
    float tIdx = floor(r.z * tilesCount);
    float tX = fmod(tIdx, Tiles.x);
    float tY = floor(tIdx / Tiles.x);

    // Rotación y escala aleatoria
    float angle = (r.x * RotJitter) * 6.2831853; // 0..2π si RotJitter=1
    float s = lerp(ScaleMin, ScaleMax, r.y);

    // Centrado, escala inversa y rotación
    float2 p = (cellUV - 0.5) / s;
    float c = cos(angle), sn = sin(angle);
    float2 uvR = float2(p.x*c - p.y*sn, p.x*sn + p.y*c) + 0.5;

    // Mapear al sub-rectángulo del atlas
    float2 tileSize = 1.0 / Tiles;
    atlasUV = uvR * tileSize + float2(tX, tY) * tileSize;
}
