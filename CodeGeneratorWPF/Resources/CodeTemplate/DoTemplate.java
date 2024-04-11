package %base_package%.entity;

import lombok.Data;
import lombok.EqualsAndHashCode;

/**
 * @Description: <br/>
 * <p>%TableComment%Do</p>
 * @Author: [%author%]
 * @Date: %date%
 * @Copyright: 2023 www.31meta.cn Inc. All rights reserved.
 */
@Data
@EqualsAndHashCode(callSuper = true)
public class %ClassName%Do extends DeletableDo {
    @EntityColumn(length = 64, comment = "所在省")
    private String province;

    @EntityColumn(length = 64, comment = "所在市")
    private String city;

    @EntityColumn(length = 64, comment = "所在区")
    private String counties;

    @EntityColumn(length = 255, comment = "详细地址")
    private String address;
}
